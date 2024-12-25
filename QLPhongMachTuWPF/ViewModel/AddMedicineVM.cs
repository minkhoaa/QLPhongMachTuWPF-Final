using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QLPhongMachTuWPF.ViewModel
{
   
    public class AddMedicineVM : ViewModelBase
    {


        private ObservableCollection<THUOC> _Medicine { get; set; }

        public ObservableCollection<THUOC> Medicine { get { return _Medicine; } set { _Medicine = value; OnPropertyChanged(); } }
        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Unit { get; set; }
        public string Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private decimal _Price { get; set; }
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private string _Status { get; set; }
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        public ICommand AddMedicineCommand { get; set; }
        public AddMedicineVM()
        {
            AddMedicineCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name)) return false;
                return true;
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị

                var newMedicine = new THUOC()
                {
                    TenThuoc = Name, 
                    Gia = (decimal)Price, 
                    DonViTinh = Unit,
                    TrangThai = (Status == "Available") ? 1 : 0

                };

                try
                {
                    // Lưu vào cơ sở dữ liệu
                    DataProvider.Ins.db.THUOCs.Add(newMedicine);
                    DataProvider.Ins.db.SaveChanges();

                    // Gửi thông báo kèm bệnh nhân mới
                    Messenger.Default.Send(newMedicine);

                    MessageBox.Show("Thêm thuốc thành công!");
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm thuốc: {ex.Message}");
                }
            });

        }
        private void ResetFields()
        {
            Name = string.Empty;
            Price =0;
            Unit = string.Empty;
            Status = string.Empty;
        }
    }
}
