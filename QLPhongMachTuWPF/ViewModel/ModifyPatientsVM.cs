using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private BENHNHAN BenhNhan { get; set; }

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
                    Thang = MonthToString(dateOfBirth.Month);
                    Nam = dateOfBirth.Year.ToString();
                });
            });

            SaveChangesCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => {
                BenhNhan.TenBN = TenBN;
                BenhNhan.DiaChi = DiaChi;
                BenhNhan.GioiTinh = Gender;
                BenhNhan.DienThoai = DienThoai;
                BenhNhan.NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay)); 
                BenhNhan.TrangThai = (Charged== "Discharged") ? 1 : 0;
                Messenger.Default.Send(BenhNhan);
                Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(w => w.IsActive)
                ?.Close();
                    });
        }
    }
    }

