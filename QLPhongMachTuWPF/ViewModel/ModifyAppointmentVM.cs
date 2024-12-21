﻿using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyAppointmentVM : ViewModelBase
    {
        private ObservableCollection<NHANVIEN> _ListStaff { get; set; }

        public ObservableCollection<NHANVIEN> ListStaff { get => _ListStaff; set { _ListStaff = value; OnPropertyChanged(); } }
        #region ThuocTinh
        private string _TenBN { get; set; }

        public string TenBN { get => _TenBN; set { _TenBN = value; OnPropertyChanged(); } }

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

        public string NgayApp { get => _NgayKham; set { _NgayKham = value; OnPropertyChanged(); } }

        private string _ThangKham { get; set; }

        public string ThangApp { get => _ThangKham; set { _ThangKham = value; OnPropertyChanged(); } }

        private string _NamKham { get; set; }

        public string NamApp { get => _NamKham; set { _NamKham = value; OnPropertyChanged(); } }
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



            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            GenderSource = new List<string> { "Male", "Female" };
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
        public ICollectionView FilteredAppointment { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        private ObservableCollection<LICHHEN> _Appointment;
        public ObservableCollection<LICHHEN> AppointmentList { get => _Appointment; set { _Appointment = value; OnPropertyChanged(); } }

        private LICHHEN LichHen { get; set; }

        private BENHNHAN benhnhan { get; set; }
        public ModifyAppointmentVM()
        {
            AddSource(); 
            AppointmentList = new ObservableCollection<LICHHEN>(DataProvider.Ins.db.LICHHENs);
            FilteredAppointment = CollectionViewSource.GetDefaultView(AppointmentList);
            
            Messenger.Default.Register<LICHHEN>(this, (appointment) =>
            {
               benhnhan = DataProvider.Ins.db.BENHNHANs.FirstOrDefault(x => x.TenBN == appointment.TenBN && x.NgaySinh == appointment.NgaySinh && x.DienThoai == appointment.DienThoai);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LichHen = appointment;
                    TenBN = appointment.TenBN;
                    DiaChi = appointment.DiaChi;
                    DienThoai = appointment.DienThoai;
                    DateTime dateofBirth = appointment.NgaySinh.Value;
                    Ngay = dateofBirth.Day.ToString(); 
                    Thang = dateofBirth.Month.ToString();
                    Nam = dateofBirth .Year.ToString();
                    DateTime dueDate = appointment.NgayKham.Value;
                    NgayApp = dueDate.Day.ToString(); 
                    ThangApp = dueDate.Month.ToString();
                    NamApp = dueDate.Year.ToString();
                    Gender = (appointment.GioiTinh == "Nam") ? "Male" : "Female";
                    Status = (appointment.TrangThai == 1) ? "Available" : "Unavailable";
                    TenNV = appointment.TenNV;
                    

                });
            });


            SaveChangesCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => {
                if (benhnhan != null)
                {
                    try
                    {
                        benhnhan.TenBN = TenBN;
                        benhnhan.DiaChi = DiaChi;
                        benhnhan.DienThoai = DienThoai;
                        benhnhan.NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Lỗi");
                    }
                    Messenger.Default.Send(benhnhan);
                } 
                else
                {
                    var newPatient = new BENHNHAN()
                    {
                        TenBN = TenBN,
                        DiaChi = DiaChi,
                        DienThoai = DienThoai,
                        NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay)),
                        GioiTinh = (Gender == "Male") ? "Nam" : "Nữ",
                        TrangThai = (Status == "Discharged") ? 1 : 0
                    };

                    DataProvider.Ins.db.BENHNHANs.Add(newPatient);

                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newPatient);
                }


                LichHen.TenBN = TenBN;
                LichHen.DiaChi = DiaChi;
                LichHen.GioiTinh = Gender;
                LichHen.DienThoai = DienThoai;
                LichHen.NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay));
                LichHen.TrangThai = (Status == "Available") ? 1 : 0;


                Messenger.Default.Send(benhnhan); 
                Messenger.Default.Send(LichHen);

                Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(w => w.IsActive)
                ?.Close();
});


        }

    }
}