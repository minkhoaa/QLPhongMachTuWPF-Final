using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddStaffsVM : ViewModelBase
    {
        #region ThuocTinh
        private ObservableCollection<NHANVIEN> _Staff { get; set; }

        public ICommand AddStaffsCommand { get; set; } 
        public ObservableCollection<NHANVIEN> Staff { get { return _Staff; } set { _Staff = value; OnPropertyChanged(); } }

        private string _Ten { get; set; }

        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _DienThoai { get; set; }

        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }

        private string _DiaChi { get; set; }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _Ngay { get; set; }

        public string Ngay { get => _Ngay; set { _Ngay = value; OnPropertyChanged(); } }

        private string _Thang { get; set; }

        public string Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }

        private string _Nam { get; set; }

        public string Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }

        private string _Gender { get; set; }

        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Type { get; set; }

        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private string _Status { get; set; }

        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }
      
        #endregion
        #region FormatBindingDate

        public List<int> Days { get; set; }
        public List<int> Months { get; set; }
        public List<int> Years { get; set; }

        public List<int> DaysApp { get; set; }
        public List<int> MonthsApp { get; set; }
        public List<int> YearsApp { get; set; }



        public List<string> GenderSource { get; set; }
        public List<string> StatusSource { get; set; }

        public List<string> TypeSource { get; set; }


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
            SelectedYear = DateTime.Now.Year;
            SelectedMonth = DateTime.Now.Month;
            Years = new List<int>();
            for (int i = 1900; i <= 2100; i++)
                Years.Add(i);
            Months = new List<int>();
            for (int i = 1; i <= 12; i++)
                Months.Add(i);
            UpdateDays();



            //  ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            GenderSource = new List<string> { "Nam", "Nữ" };
            StatusSource = new List<string> { "Discharged", "Under treatment" };
            TypeSource = new List<string> { "User", "Admin" };
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
        public AddStaffsVM() {
            AddSource(); 

            AddStaffsCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(DienThoai) || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(Ngay) || string.IsNullOrEmpty(Thang)
                || string.IsNullOrEmpty(Nam) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Status) || string.IsNullOrEmpty(Type)
                 ) return false;
                return true;
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị
                try { 
                var newStaff = new NHANVIEN()
                {
                    TenNV = Ten,
                    NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay)),
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    LoaiNV = (Type == "Admin") ? 0 : 1,
                    GioiTinh = Gender,
                    TrangThai = (Status == "Discharged") ? 1 : 0
                }; 

                        // Lưu vào cơ sở dữ liệu
                        DataProvider.Ins.db.NHANVIENs.Add(newStaff);
                        DataProvider.Ins.db.SaveChanges();

                        // Gửi thông báo kèm bệnh nhân mới
                        Messenger.Default.Send(newStaff);

                        MessageBox.Show("Thêm nhân viên thành công!");
                    Messenger.Default.Send("NewStaffAdded");
                    ResetFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}");
                    }
                });
            }
        private void ResetFields()
        {
            Ten = string.Empty;
            DiaChi = string.Empty;
            DienThoai = string.Empty;
          
            Status = string.Empty;
        }

    }
}


