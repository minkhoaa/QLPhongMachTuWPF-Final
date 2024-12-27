using GalaSoft.MvvmLight.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddAppointmentVM : ViewModelBase
    {
        #region ThuocTinh
        private string _TenBN { get; set; }

        public string TenBN { get => _TenBN; set { _TenBN = value; OnPropertyChanged(); } }
        private string _ThoiGian { get; set; }

        public string ThoiGian { get => _ThoiGian; set { _ThoiGian = value; OnPropertyChanged(); } }

        private string _DiaChi { get; set; }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }


        private string _DienThoai { get; set; }

        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }


        private string _Ngay { get; set; }

        public string Ngay { get => _Ngay; set { _Ngay = value; OnPropertyChanged(); } }

        private string _Thang { get; set; }

        public string Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }

        private string _Nam { get; set; }

        public string Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }

        private string _Gender { get; set; }

        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Status { get; set; }

        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }
        private string _TenNV { get; set; }

        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }

        private string _NgayKham { get; set; }

        public string NgayKham { get => _NgayKham; set { _NgayKham = value; OnPropertyChanged(); } }

        private string _ThangKham { get; set; }

        public string ThangKham { get => _ThangKham; set { _ThangKham = value; OnPropertyChanged(); } }

        private string _NamKham { get; set; }

        public string NamKham { get => _NamKham; set { _NamKham = value; OnPropertyChanged(); } }
        #endregion

        private ObservableCollection<NHANVIEN> _ListStaff { get; set; }

        public ObservableCollection<NHANVIEN> ListStaff { get => _ListStaff; set { _ListStaff = value; OnPropertyChanged(); } }

        private NHANVIEN _SelectedItemCommand {  get; set; }
        
        public NHANVIEN SelectedItemCommand { get => _SelectedItemCommand; set { _SelectedItemCommand = value ; OnPropertyChanged(); } }
        public ICommand AddAppointmentCommand { get; set; }


        #region FormatBindingDate

        public List<int> Days { get; set; }
        public List<int> Months { get; set; }
        public List<int> Years { get; set; }

        public List<int> DaysApp { get; set; }
        public List<int> MonthsApp { get; set; }
        public List<int> YearsApp { get; set; }

        public List<string> TimeSource { get; set; }
     
        public List<string> GenderSource { get; set; }
        public List<string> StatusSource { get; set; }

        private int _selectedDay;
        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                OnPropertyChanged();
            }
        }
        private int _selectedMonth;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
                UpdateDays();
                // Cập nhật số ngày khi tháng thay đổi
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
                UpdateDays();
                // Cập nhật số ngày khi năm thay đổi
            }
        }


        private int _selectedDayApp;
        public int SelectedDayApp
        {
            get => _selectedDayApp;
            set
            {
                _selectedDayApp = value;
                OnPropertyChanged();
            }
        }
        private int _selectedMonthApp;
        public int SelectedMonthApp
        {
            get => _selectedMonthApp;
            set
            {
                _selectedMonthApp = value;
                OnPropertyChanged();
                UpdateDaysApp();
                // Cập nhật số ngày khi tháng thay đổi
            }
        }

        private int _selectedYearApp;
        public int SelectedYearApp
        {
            get => _selectedYearApp;
            set
            {
                _selectedYearApp = value;
                OnPropertyChanged();
                UpdateDaysApp();

            }
        }


        public void AddSource()
        {
            TimeSource = new List<string>(); 
            for (int i = 7; i <= 16; i++ )
            {
                if (i != 11 || i != 12)
                    TimeSource.Add(i.ToString() + ":" + "30");
            }
            SelectedYear = DateTime.Now.Year;
            SelectedMonth = DateTime.Now.Month;
            Years = new List<int>();
            for (int i = 1900; i <= 2100; i++)
                Years.Add(i);
            Months = new List<int>();
            for (int i = 1; i <= 12; i++)
                Months.Add(i);
            UpdateDays();



            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            GenderSource = new List<string> {"Nam", "Nữ" };
            StatusSource = new List<string> { "Available", "Unavailable" };

            // Khởi tạo số ngày theo tháng và năm mặc định  a


            SelectedYearApp = DateTime.Now.Year;
            SelectedMonthApp = DateTime.Now.Month;
            YearsApp = new List<int>();
            for (int i = 1900; i <= 2100; i++)
                YearsApp.Add(i);
            MonthsApp = new List<int>();
            for (int i = 1; i <= 12; i++)
                MonthsApp.Add(i);
            UpdateDaysApp();
        }
        private void UpdateDays()
        {
            try
            {
                if (SelectedYear < 1 || SelectedYear > 9999)
                    throw new ArgumentOutOfRangeException(nameof(SelectedYear), "Year must be between 1 and 9999.");

                int daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

                Days = new List<int>();
                for (int i = 1; i <= daysInMonth; i++)
                    Days.Add(i);

                if (SelectedDay > daysInMonth)
                    SelectedDay = daysInMonth;

                OnPropertyChanged(nameof(Days));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating days: {ex.Message}");
            }
        }
        private void UpdateDaysApp()
        {
            try
            {
                if (SelectedYearApp < 1 || SelectedYearApp > 9999)
                    throw new ArgumentOutOfRangeException(nameof(SelectedYearApp), "Year must be between 1 and 9999.");

                int daysInMonthApp = DateTime.DaysInMonth(SelectedYearApp, SelectedMonthApp);

                DaysApp = new List<int>();
                for (int i = 1; i <= daysInMonthApp; i++)
                    DaysApp.Add(i);

                if (SelectedDayApp > daysInMonthApp)
                    SelectedDayApp = daysInMonthApp;

                OnPropertyChanged(nameof(DaysApp));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating days: {ex.Message}");
            }
        }
        #endregion

        public AddAppointmentVM()
        {
            AddSource(); 
            AddAppointmentCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenNV) || string.IsNullOrEmpty(TenBN)
                || string.IsNullOrEmpty(DienThoai) || string.IsNullOrEmpty(DiaChi)
                || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Ngay)
                || string.IsNullOrEmpty(Thang) || string.IsNullOrEmpty(Nam)
                || string.IsNullOrEmpty(NgayKham) || string.IsNullOrEmpty(ThangKham) || string.IsNullOrEmpty(NamKham)
                || string.IsNullOrEmpty(ThoiGian)
                ) return false;
                return true;
            }, (p) => {
                // Khởi tạo đối tượng mới
                var newPatient = new BENHNHAN()
                {
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay)),
                    GioiTinh = Gender,
                    TrangThai = (Status == "Discharged") ? 1 : 0
                };
                try
                {
                    DataProvider.Ins.db.BENHNHANs.Add(newPatient);
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newPatient);
                }
                catch
                (Exception )
                {
                    MessageBox.Show("fail to add patient");
                }
                PHIEUKHAM newDiagnosis = new PHIEUKHAM()
                {
                    MaNV = SelectedItemCommand.MaNV,
                    MaBN = newPatient.MaBN,

                    NgayKham = new DateTime(int.Parse(NamKham), int.Parse(ThangKham), int.Parse(NgayKham)),
                    TrieuChung = "",
                    KetQua = "",
                    TrangThai = 1,
                };
                try
                {
                    // Đảm bảo Messenger đã được đăng ký và đối tượng DiagnosisVM đã sẵn sàng
                    DataProvider.Ins.db.PHIEUKHAMs.Add(newDiagnosis);
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newDiagnosis);

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                // Tạo lịch hẹn mới
                var newAppointment = new LICHHEN()
                {
                    MaBN = newPatient.MaBN,
                    MaNV = SelectedItemCommand.MaNV,
                    NgayHen = new DateTime(int.Parse(NamKham), int.Parse(ThangKham), int.Parse(NgayKham)),
                    GioHen = TimeSpan.Parse(ThoiGian),
                    MaPK = newDiagnosis.MaPK
                };

                // Tạo đối tượng PHIEUKHAM và gửi
              
                try
                {
                    // Lưu vào cơ sở dữ liệu
                    
                    DataProvider.Ins.db.LICHHENs.Add(newAppointment);
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newAppointment);
                    MessageBox.Show("Thêm lịch hẹn thành công!");
                    Messenger.Default.Send("NewAppointmentAdded");

                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm lịch hẹn: {ex.Message}");
                }
                Application.Current.Windows
              .OfType<Window>()
              .SingleOrDefault(w => w.IsActive)
              ?.Close();
            });
        }
        private void ResetFields()
        {
            TenBN = string.Empty;
            DiaChi = string.Empty;
            DienThoai = string.Empty;
          
            TenNV = string.Empty;
        }

    }
}
