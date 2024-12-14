using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Net.Mime;
using System.Windows.Media;
using System.Windows.Documents.DocumentStructures;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyMedicineVM : ViewModelBase
    {
        public ICommand SaveChangesCommand { get; set; }

        private string _Name { get; set; }
        public string Name
        {
            get => _Name; set
            {
                _Name = value; OnPropertyChanged();
            }
        }
        private string _Price { get; set; }
        public string Price
        {
            get => _Price; set
            {
                _Price = value; OnPropertyChanged();
            }
        }
        private string _Unit { get; set; }
        public string Unit
        {
            get => _Unit; set
            {
                _Unit = value; OnPropertyChanged();
            }
        }

        private string _Status { get; set; }
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private THUOC Thuoc { get; set; }



        public ModifyMedicineVM()
        {
            Messenger.Default.Register<THUOC>(this, (medicine) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Thuoc = medicine; 
                    Status = (medicine.TrangThai == 1) ? "Available" : "Unavailable"; 
                    Price = medicine.Gia.ToString();
                    Unit = medicine.DonViTinh.ToString();
                    Name = medicine.TenThuoc.ToString();
                });
            });

            SaveChangesCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(Name))
                    return false;

                // Kiểm tra xem khóa chính mới có tồn tại không
                var existingMedicine = DataProvider.Ins.db.THUOCs.FirstOrDefault(t => t.TenThuoc == Name);
                if (existingMedicine != null && existingMedicine.TenThuoc != Thuoc.TenThuoc)
                    return false;

                return true;
            }, (p) =>
            {
                try
                {
                    using (var transaction = DataProvider.Ins.db.Database.BeginTransaction())
                    {
                        // Cập nhật thông tin các bản ghi liên quan trong bảng CTTT
                        var relatedRecords = DataProvider.Ins.db.CTTTs.Where(c => c.TenThuoc == Thuoc.TenThuoc).ToList();
                        foreach (var record in relatedRecords)
                        {
                            record.TenThuoc = Name;
                        }

                        // Tạo thực thể mới với giá trị mới
                        var newMedicine = new THUOC
                        {
                            TenThuoc = Name,
                            Gia = decimal.Parse(Price),
                            DonViTinh = Unit,
                            TrangThai = (Status == "Available") ? 1 : 0
                        };

                        // Thêm thực thể mới vào database
                        DataProvider.Ins.db.THUOCs.Add(newMedicine);

                        // Xóa thực thể cũ
                        DataProvider.Ins.db.THUOCs.Remove(Thuoc);

                        // Lưu thay đổi
                        DataProvider.Ins.db.SaveChanges();

                        // Cam kết giao dịch
                        transaction.Commit();

                        // Gửi thông báo cập nhật
                        Messenger.Default.Send(newMedicine);

                        // Đóng cửa sổ
                        Application.Current.Windows
                            .OfType<Window>()
                            .SingleOrDefault(w => w.IsActive)
                            ?.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating medicine: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        }


    }
}
