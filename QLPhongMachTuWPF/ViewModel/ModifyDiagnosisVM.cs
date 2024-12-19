using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyDiagnosisVM  : ViewModelBase
    {
        public ICommand ModifyDiagnosisCommand { get; set; }

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
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
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

        public ModifyDiagnosisVM()
        {
            Messenger.Default.Register<LICHHEN>(this, (diagnosis) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                     TenNV =  diagnosis.TenNV;
                     TenBN = diagnosis.TenBN;
                    DateTime dateOfBirth = diagnosis.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = MonthToString(dateOfBirth.Month);
                    Nam = dateOfBirth.Year.ToString();
                    DateTime dateTreat = diagnosis.NgayKham.Value;
                    NgayKham = dateTreat.Day.ToString();
                    ThangKham = MonthToString(dateTreat.Month);
                    NamKham = dateTreat.Year.ToString();

                    DiaChi = diagnosis.DiaChi;
                    DienThoai = diagnosis.DienThoai;
                    Gender = diagnosis.GioiTinh;
                    Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";  
                    
                });
            });

        }
    }
}
