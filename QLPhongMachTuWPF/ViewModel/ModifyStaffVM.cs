using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyStaffVM : ViewModelBase
    {
        public ICommand SaveChangesStaffCommand { get; set; }


        public ICommand SaveChangesCommand { get; set; }

        private string _TenNV { get; set; }
        public string TenNV
        {
            get => _TenNV; set
            {
                _TenNV = value; OnPropertyChanged();
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

        private string _Status { get; set; }
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private string _Type { get; set; }
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        public DateTime? date { get; set; }
        private ObservableCollection<NHANVIEN> _staff { get; set; }

        public ObservableCollection<NHANVIEN> Staff { get { return _staff; } set { _staff = value; OnPropertyChanged(); } }

        private NHANVIEN NhanVien { get; set; }
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

        public ModifyStaffVM()
        {
            Messenger.Default.Register<NHANVIEN>(this, (staff) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    NhanVien = staff;
                    TenNV = staff.TenNV;
                    DiaChi = staff.DiaChi;
                    DienThoai = staff.DienThoai;
                    Gender = staff.GioiTinh;

                    DateTime dateOfBirth = staff.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = MonthToString(dateOfBirth.Month);
                    Nam = dateOfBirth.Year.ToString();
                    Type = (staff.LoaiNV == 0) ? "Admin" : "User";
                    Status = (staff.TrangThai == 1) ? "Onboard" : "Rejected";
                });
            }); 

                SaveChangesStaffCommand = new RelayCommand<object>((p) =>
                {
                    return true;
                }, (p) => {

                    NhanVien.TenNV = TenNV;
                    NhanVien.DiaChi = DiaChi;
                    NhanVien.GioiTinh = Gender;
                    NhanVien.DienThoai = DienThoai;
                    NhanVien.NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay));
                    NhanVien.TrangThai = (Status == "Onboard") ? 1 : 0;
                    NhanVien.LoaiNV = (Type == "Admin") ? 0 : 1;

                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(NhanVien);
                    Application.Current.Windows
                    .OfType<Window>()
                    .SingleOrDefault(w => w.IsActive)
                    ?.Close();
                });
        }
            
        
    }
}
