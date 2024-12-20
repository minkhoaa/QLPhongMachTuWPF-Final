using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyPatientsVM : ViewModelBase
    {
       public ICommand SaveChangesCommand {  get; set; }
        #region ThuocTinh
        private string _TenBN {  get; set; }
        public string TenBN
        {
            get => _TenBN; set
            {
                _TenBN = value; OnPropertyChanged();
            }
        }

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

        private string _Charged { get; set; }
        public string Charged { get => _Charged; set { _Charged = value; OnPropertyChanged(); } }

        public DateTime? date { get; set; }

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
            GenderSource = new List<string> { "Male", "Female" };
            StatusSource = new List<string> { "Discharged", "Under treatment" };

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
        private BENHNHAN BenhNhan { get; set; }

        LICHHEN lichhen { get; set; }

        PHIEUKHAM phieukham   { get; set; }
        public int CheckMonth(string Thang)
        {
            switch (Thang)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    return -1;
            }
        }

        public string MonthToString(int thang)
        {
          
                switch (thang)
                {
                    case 1 :
                        return "January";
                    case 2 :
                        return "February";
                    case 3:
                    return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6 :
                    return "June"; 
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9 :
                    return "September";
                    case 10:
                    return "October";
                    case 11:
                    return "November";
                    case 12:
                    return "December";
                    default:
                        return "null";
                }
            }
        
        public ModifyPatientsVM()
        {
           
            // Đăng ký nhận thông báo về bệnh nhân
            Messenger.Default.Register<BENHNHAN>(this, (patient) =>
            {
                lichhen = DataProvider.Ins.db.LICHHENs.FirstOrDefault(x => x.TenBN == patient.TenBN && x.NgaySinh == patient.NgaySinh);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    BenhNhan = patient;
                    TenBN = patient.TenBN;
                    DiaChi = patient.DiaChi;
                    DienThoai = patient.DienThoai;
                    Gender = patient.GioiTinh;
                    Charged = (patient.TrangThai == 1) ? "Discharged" : "Under treatment" ;
                    DateTime dateOfBirth = patient.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = dateOfBirth.Month.ToString();
                    Nam = dateOfBirth.Year.ToString();
                });
            });

            SaveChangesCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => {
                if (lichhen != null)
                {
                    try
                    {
                        lichhen.TenBN = TenBN;
                        lichhen.DiaChi = DiaChi;
                        lichhen.DienThoai = DienThoai;
                        lichhen.NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay));
                        lichhen.GioiTinh = Gender;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi");
                    }
                    Messenger.Default.Send(lichhen);
                }
              
                BenhNhan.TenBN = TenBN;
                BenhNhan.DiaChi = DiaChi;
                BenhNhan.GioiTinh = Gender;
                BenhNhan.DienThoai = DienThoai;
                BenhNhan.NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay));
                BenhNhan.TrangThai = (Charged == "Discharged") ? 1 : 0;

                // Gửi thông tin bệnh nhân đã cập nhật qua Messenger
               
                Messenger.Default.Send(BenhNhan);

                // Đóng cửa sổ
                Application.Current.Windows
                    .OfType<Window>()
                    .SingleOrDefault(w => w.IsActive)
                    ?.Close();
            });
        }
    }
    }

