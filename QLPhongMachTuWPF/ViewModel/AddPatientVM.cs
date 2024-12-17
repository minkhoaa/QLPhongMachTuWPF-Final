using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddPatientVM : ViewModelBase   
    {
        private ObservableCollection<BENHNHAN> _patients { get ; set; }

        public ObservableCollection<BENHNHAN> Patients { get { return _patients; } set { _patients = value; OnPropertyChanged();  } }
        private string _TenBN {  get; set; }

        public string TenBN { get => _TenBN; set { _TenBN = value; OnPropertyChanged();  } }

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

        
        public ICommand AddPatientCommand { get; set; }
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
        public AddPatientVM()
        {
            CheckMonth(Thang); 
            AddPatientCommand = new RelayCommand<object>((p)=>  
            {
                if(string.IsNullOrEmpty(TenBN)) return false;
                return true;    
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị

                var newPatient = new BENHNHAN()
                {  
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay)),
                    GioiTinh = Gender,
                    TrangThai = (Charged == "Discharged") ? 1 : 0
                }; 

                try
                {
                    // Lưu vào cơ sở dữ liệu
                    DataProvider.Ins.db.BENHNHANs.Add(newPatient);
                    DataProvider.Ins.db.SaveChanges();
                    // Gửi thông báo kèm bệnh nhân mới
                    Messenger.Default.Send(newPatient);

                    MessageBox.Show("Thêm bệnh nhân thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm bệnh nhân: {ex.Message}");
                }
            });
        }

    }
}
