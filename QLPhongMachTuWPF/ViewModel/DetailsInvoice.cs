﻿using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLPhongMachTuWPF.ViewModel
{
    public class DetailsInvoice : ViewModelBase
    {
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

        private string _NgayKham { get; set; }

        public string NgayKham { get => _NgayKham; set { _NgayKham = value; OnPropertyChanged(); } }

        private string _ThangKham { get; set; }

        public string ThangKham { get => _ThangKham; set { _ThangKham = value; OnPropertyChanged(); } }

        private string _NamKham { get; set; }

        public string NamKham { get => _NamKham; set { _NamKham = value; OnPropertyChanged(); } }

        private string _TenNV { get; set; }

        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }

        private string _Symtoms { get; set; }

        public string Symtoms { get => _Symtoms; set { _Symtoms = value; OnPropertyChanged(); } }

        private string _Result { get; set; }

        public string Result { get => _Result; set { _Result = value; OnPropertyChanged(); } }

        #endregion
        private ObservableCollection<NHANVIEN> _ListStaff { get; set; }

        private string _ID { get; set; }

        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        private string _NgayHoanThanh { get; set; }

        public string NgayHoanThanh { get => _NgayHoanThanh; set { _NgayHoanThanh = value; OnPropertyChanged(); } }

        public ObservableCollection<NHANVIEN> ListStaff { get => _ListStaff; set { _ListStaff = value; OnPropertyChanged(); } }

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
            GenderSource = new List<string> { "Nam", "Nữ" };
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

        public DetailsInvoice()
        {
            AddSource();
            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            Messenger.Default.Register<LICHHEN>(this, (diagnosis) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    TenNV = diagnosis.TenNV;
                    TenBN = diagnosis.TenBN;
                    DateTime dateOfBirth = diagnosis.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = dateOfBirth.Month.ToString();
                    Nam = dateOfBirth.Year.ToString();
                    DateTime dateTreat = diagnosis.NgayKham.Value;
                    NgayKham = dateTreat.Day.ToString();
                    ThangKham = dateTreat.Month.ToString();
                    NamKham = dateTreat.Year.ToString();
                    
                    DiaChi = diagnosis.DiaChi;
                    DienThoai = diagnosis.DienThoai;
                    Gender = diagnosis.GioiTinh;
                    Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";
                });
            });
            Messenger.Default.Register<PHIEUKHAM>(this, (diagnosis) =>
            {
                var Staff = DataProvider.Ins.db.NHANVIENs.FirstOrDefault(a => a.MaNV == diagnosis.MaNV);
                var Patient = DataProvider.Ins.db.BENHNHANs.FirstOrDefault(a => a.MaBN == diagnosis.MaBN);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ID = diagnosis.MaPK.ToString();
                    TenNV = Staff.TenNV;
                    TenBN = Patient.TenBN;
                    DateTime dateOfBirth = Patient.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = dateOfBirth.Month.ToString();
                    Nam = dateOfBirth.Year.ToString();
                    DateTime dateTreat = diagnosis.NgayKham.Value;
                    NgayKham = dateTreat.Day.ToString();
                    ThangKham = dateTreat.Month.ToString();
                    NamKham = dateTreat.Year.ToString();
                    NgayHoanThanh = dateTreat.ToString();   
                    DiaChi = Patient.DiaChi;
                    DienThoai = Patient.DienThoai;
                    Gender = Patient.GioiTinh;
                    Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";
                    Symtoms = diagnosis.TrieuChung;
                    Result = diagnosis.KetQua;

                });
            });

        }

    }
}