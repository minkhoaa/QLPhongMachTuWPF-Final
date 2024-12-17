using GalaSoft.MvvmLight.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddAppointmentVM : ViewModelBase
    {
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

        public string NgayKham { get => _NgayKham; set { _NgayKham = value; OnPropertyChanged(); } }

        private string _ThangKham { get; set; }

        public string ThangKham { get => _ThangKham; set { _ThangKham = value; OnPropertyChanged(); } }

        private string _NamKham { get; set; }

        public string NamKham { get => _NamKham; set { _NamKham = value; OnPropertyChanged(); } }


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
        public ICommand AddAppointmentCommand { get; set; }


        public AddAppointmentVM() {
            AddAppointmentCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenBN)) return false;
                return true;
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị


                var newPatient = new BENHNHAN()
                {
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay)),
                    GioiTinh = (Gender == "Male") ? "Nam" :  "Nữ",
                    TrangThai = (Status == "Discharged") ? 1 : 0
                };
                var newAppointment = new LICHHEN()
                {
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay)),
                    GioiTinh = (Gender == "Male") ? "Nam" : "Nữ",
                    TrangThai = (Status == "Discharged") ? 1 : 0,
                    TenNV = TenNV,
                    NgayKham = new DateTime(int.Parse(NamKham), CheckMonth(ThangKham), int.Parse(NgayKham))
                };

                try
                {
                    // Lưu vào cơ sở dữ liệu
                    DataProvider.Ins.db.LICHHENs.Add(newAppointment);
                    DataProvider.Ins.db.BENHNHANs.Add(newPatient); 
                    DataProvider.Ins.db.SaveChanges();

                    // Gửi thông báo kèm bệnh nhân mới
                    //Messenger.Default.Send(newPatient);
                    Messenger.Default.Send(newAppointment);

                    MessageBox.Show("Thêm lịch hẹn thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm lịch hẹn: {ex.Message}");
                }
            });

        }
    }
}
