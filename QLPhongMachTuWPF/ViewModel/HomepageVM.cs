using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class HomepageVM : ViewModelBase
    {
        public string FormattedTotalIncome
        {
            get => $"{TotalIncome:N0}đ"; // Định dạng số với dấu phân cách hàng nghìn và thêm 'đ'
        }

        //Tổng số tiền hóa đơn
        private int? _totalIncome;
        public int? TotalIncome
        {
            get => _totalIncome;
            set
            {
                _totalIncome = value;
                OnPropertyChanged();
            }
        }


        // Tổng số staffs
        private int _totalStaffs;
        public int TotalStaffs
        {
            get => _totalStaffs;
            set
            {
                _totalStaffs = value;
                OnPropertyChanged();
            }
        }

        // Tổng số patients
        private int _totalPatients;
        public int TotalPatients
        {
            get => _totalPatients;
            set
            {
                _totalPatients = value;
                OnPropertyChanged();
            }
        }

        // Thuộc tính lưu ngày được chọn từ DatePicker
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();

                // Tự động tải lịch hẹn cho ngày được chọn
                if (_selectedDate.HasValue)
                {
                    LoadAppointmentsByDate(_selectedDate.Value);

                    // Gọi hàm hiển thị bệnh nhân trong tuần
                    LoadWeeklyPatientCounts(_selectedDate.Value);
                }
            }
        }


        // Thuộc tính lưu danh sách lịch hẹn
        private ObservableCollection<Appointment> _appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged();
            }
        }

        // Danh sách bệnh nhân kèm thông tin chẩn đoán
        private ObservableCollection<PatientWithDiagnosis> _patientsWithDiagnosis;
        public ObservableCollection<PatientWithDiagnosis> PatientsWithDiagnosis
        {
            get => _patientsWithDiagnosis;
            set { _patientsWithDiagnosis = value; OnPropertyChanged(); }
        }

        // Thuộc tính dữ liệu cho biểu đồ
        private ChartValues<int> _customerCounts;
        private List<string> _dayLabels;

        public ChartValues<int> CustomerCounts
        {
            get => _customerCounts;
            set { _customerCounts = value; OnPropertyChanged(); }
        }

        public List<string> DayLabels
        {
            get => _dayLabels;
            set { _dayLabels = value; OnPropertyChanged(); }
        }

        // Lệnh (Command)
        public ICommand LoadChartDataCommand { get; set; }

        public HomepageVM()
        {
            // Khởi tạo lệnh
            TotalIncome = 0; 
            LoadChartDataCommand = new RelayCommand<object>(CanLoadChartData, LoadChartData);

            // Khởi tạo giá trị mặc định cho các thuộc tính
            CustomerCounts = new ChartValues<int>();
            DayLabels = new List<string>();

            // Tải dữ liệu cho DataGrid
            LoadCombinedData();

            // Tự động tải dữ liệu biểu đồ
            LoadChartData(null);

            // Khởi tạo danh sách lịch hẹn
            Appointments = new ObservableCollection<Appointment>();

            // Đặt ngày mặc định là hôm nay
            SelectedDate = DateTime.Today;

            // Tải dữ liệu mặc định
            LoadAppointmentsByDate(SelectedDate.Value);

            // Tính tổng staffs và patients
            LoadTotalCounts();

            LoadWeeklyPatientCounts(SelectedDate.Value);

            Messenger.Default.Register<string>(this, (message) =>
            {
                if (message == "NewPatientAdded")
                {
                    // Cập nhật TotalPatients (tăng thêm 1)
                    TotalPatients += 1;
                }

                if (message == "NewStaffAdded")
                {
                    TotalStaffs += 1;
                }

                if (message == "NewAppointmentAdded")
                {
                    // Cập nhật biểu đồ
                    LoadChartData(null);

                    // Cập nhật danh sách lịch hẹn (nếu cần)
                    if (SelectedDate.HasValue)
                    {
                        LoadAppointmentsByDate(SelectedDate.Value);
                    }
                }
            });

        }

        // Hàm tính tổng số staff và patient
        private void LoadTotalCounts()
        {
            // Lấy tổng số staffs từ cơ sở dữ liệu
            TotalStaffs = DataProvider.Ins.db.NHANVIENs.Count();

            // Lấy tổng số patients từ cơ sở dữ liệu
            TotalPatients = DataProvider.Ins.db.BENHNHANs.Count();

            
            //Lấy tổng số tiền income từ csdl
            TotalIncome = (int?)DataProvider.Ins.db.HOADONs
        .Where(hd => hd.TrangThai == 1)
        .Sum(hd => hd.TongTien);
        }

        private void LoadAppointmentsByDate(DateTime date)
        {
            // Lọc dữ liệu từ cơ sở dữ liệu
            var filteredAppointments = DataProvider.Ins.db.LICHHENs
                .Where(a => DbFunctions.TruncateTime(a.NgayKham) == DbFunctions.TruncateTime(date))
                .Select(a => new Appointment
                {

                    PatientName = a.TenBN,
                    AppointmentDate = (DateTime)a.NgayKham,

                })
                .ToList();

            // Cập nhật danh sách
            Appointments = new ObservableCollection<Appointment>(filteredAppointments);
        }

        private bool CanLoadChartData(object parameter)
        {
            return true; // Luôn có thể tải dữ liệu biểu đồ
        }

        // Hàm tải dữ liệu biểu đồ
        private void LoadChartData(object parameter)
        {
            var (startOfMonth, endOfMonth) = GetCurrentMonthRange(SelectedDate ?? DateTime.Today);
            var customerCounts = GetCustomerCountsPerDay(startOfMonth, endOfMonth);

            // Cập nhật dữ liệu biểu đồ
            CustomerCounts.Clear();
            DayLabels.Clear();

            for (int i = 0; i < (endOfMonth - startOfMonth).Days + 1; i++)
            {
                var date = startOfMonth.AddDays(i);
                DayLabels.Add(date.ToString("dd/MM"));
                CustomerCounts.Add(customerCounts.ContainsKey(date) ? customerCounts[date] : 0);
            }
        }

        // Hàm lấy số lượng khách hàng theo ngày trong tuần
        private Dictionary<DateTime, int> GetCustomerCountsPerDay(DateTime startOfMonth, DateTime endOfMonth)
        {
            var customerCounts = DataProvider.Ins.db.PHIEUKHAMs
                .Where(pk => pk.NgayKham >= startOfMonth && pk.NgayKham <= endOfMonth) // Lọc các ngày khám trong tháng
                .GroupBy(pk => DbFunctions.TruncateTime(pk.NgayKham)) // Nhóm theo ngày
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToDictionary(g => g.Date.Value, g => g.Count); // Chuyển thành Dictionary

            return customerCounts;
        }

        // Hàm tính ngày đầu tuần (Thứ Hai) và cuối tuần (Thứ Sáu)
        private (DateTime, DateTime) GetCurrentMonthRange(DateTime referenceDate)
        {
            DateTime startOfMonth = new DateTime(referenceDate.Year, referenceDate.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Ngày cuối tháng
            return (startOfMonth, endOfMonth);
        }

        // Hàm tải dữ liệu kết hợp (DataGrid)
        private void LoadCombinedData()
        {
            var combinedData = from bn in DataProvider.Ins.db.BENHNHANs
                               join pk in DataProvider.Ins.db.PHIEUKHAMs
                               on bn.MaBN equals pk.MaBN
                               select new PatientWithDiagnosis
                               {
                                   MaBN = bn.MaBN,
                                   TenBN = bn.TenBN,
                                   DOB = (DateTime)bn.NgaySinh,
                                   Phone = bn.DienThoai,
                                   Gender = bn.GioiTinh,
                                   NgayKham = (DateTime)pk.NgayKham
                               };

            PatientsWithDiagnosis = new ObservableCollection<PatientWithDiagnosis>(combinedData);
        }

        private void LoadWeeklyPatientCounts(DateTime selectedDate)
        {
            // Thay đổi để hiển thị dữ liệu của cả tháng
            var (startOfMonth, endOfMonth) = GetCurrentMonthRange(selectedDate);

            var dailyPatientCounts = DataProvider.Ins.db.PHIEUKHAMs
                .Where(pk => pk.NgayKham >= startOfMonth && pk.NgayKham <= endOfMonth) // Lấy dữ liệu trong tháng
                .GroupBy(pk => DbFunctions.TruncateTime(pk.NgayKham))
                .Select(g => new
                {
                    Date = g.Key.Value,
                    Count = g.Count()
                })
                .ToList();

            CustomerCounts.Clear();
            DayLabels.Clear();

            for (int i = 0; i < (endOfMonth - startOfMonth).Days + 1; i++)
            {
                var date = startOfMonth.AddDays(i);
                DayLabels.Add(date.ToString("dd/MM"));
                var count = dailyPatientCounts.FirstOrDefault(d => d.Date == date)?.Count ?? 0;
                CustomerCounts.Add(count);
            }
        }


    }

    // Lớp model cho bệnh nhân kèm chẩn đoán
    public class PatientWithDiagnosis
    {
        public int MaBN { get; set; }
        public string TenBN { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime NgayKham { get; set; }
    }

    //Lớp model cho appointment
    public class Appointment
    {

        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }

    }
}
