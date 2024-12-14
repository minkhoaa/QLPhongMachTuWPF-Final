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
                return true;
            }, (p) => {

                Thuoc.TrangThai = (Status == "Available") ? 1 : 0;
                Thuoc.Gia = decimal.Parse(Price);
                Thuoc.TenThuoc = Name.ToString(); 
                Thuoc.DonViTinh = Unit.ToString();

                DataProvider.Ins.db.SaveChanges();
                Messenger.Default.Send(Thuoc);
                Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(w => w.IsActive)
                ?.Close();
            });
        }


    }
}
