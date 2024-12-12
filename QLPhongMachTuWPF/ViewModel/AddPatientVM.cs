using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
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

        private DateTime _NgaySinh { get; set; }

        public DateTime NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }

        public ICommand AddPatientCommand { get; set; }
        public AddPatientVM()
        {
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
                    NgaySinh = new DateTime(2000, 1, 1),
                    GioiTinh = "1",
                    TrangThai = 1
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
        public AddPatientVM(ObservableCollection<BENHNHAN> patientsList)
        {
            Patients = patientsList;

            AddPatientCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenBN)) return false;
                return true;
            }, (p) =>
            {
          //      var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị

                var newPatient = new BENHNHAN()
                {
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(2000, 1, 1),
                    GioiTinh = "1",
                    TrangThai = 1
                };

                DataProvider.Ins.db.BENHNHANs.Add(newPatient);
                DataProvider.Ins.db.SaveChanges();

                // Thêm bệnh nhân mới vào danh sách
                Patients.Add(newPatient);
            });
        }
    }
}
