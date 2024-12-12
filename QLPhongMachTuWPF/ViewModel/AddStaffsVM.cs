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

        public AddStaffsVM() {


            AddStaffsCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Ten)) return false;
                return true;
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị

                var newStaff = new NHANVIEN()
                {
                    TenNV = Ten,
                    NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay)),
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    LoaiNV = (Type == "Admin") ? 0 : 1,
                    GioiTinh = Gender,
                    TrangThai = 1
                };

                    try
                    {
                        // Lưu vào cơ sở dữ liệu
                        DataProvider.Ins.db.NHANVIENs.Add(newStaff);
                        DataProvider.Ins.db.SaveChanges();

                        // Gửi thông báo kèm bệnh nhân mới
                        Messenger.Default.Send(newStaff);

                        MessageBox.Show("Thêm nhân viên thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}");
                    }
                });
            }
        }
    }


