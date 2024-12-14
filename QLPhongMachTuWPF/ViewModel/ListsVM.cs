using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ListsVM : ViewModelBase
    {
        public ICommand AddMedicineCommand { get; set; }

        public ICommand ModifyMedicineCommand { get; set; }

        private ObservableCollection<THUOC> _medicine;
        public ObservableCollection<THUOC> MedicineList { get => _medicine; set { _medicine = value; OnPropertyChanged(); } }

        public ICollectionView FilteredMedicine { get; set; }

        private THUOC _SelectedItemCommand {  get; set; }
        public THUOC SelectedItemCommand
        {
            get => _SelectedItemCommand;
            set
            {
                if (_SelectedItemCommand != value)
                {
                    _SelectedItemCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public ListsVM()
        {
            MedicineList = new ObservableCollection<THUOC>(DataProvider.Ins.db.THUOCs);

            FilteredMedicine = CollectionViewSource.GetDefaultView(MedicineList);


            Messenger.Default.Register<THUOC>(this, (medicine) =>
            {
                if (medicine == null) return;

                // Tìm đối tượng trong danh sách hiện tại
                var existingStaff = MedicineList.FirstOrDefault(p => p.TenThuoc == medicine.TenThuoc);
                if (existingStaff != null)
                {
                    // Cập nhật thông tin
                    
                }
                else
                {
                    // Thêm nhân viên mới
                    MedicineList.Add(medicine);
                }

                // Ghi thay đổi vào database
                DataProvider.Ins.db.SaveChanges();
                FilteredMedicine?.Refresh();
            });

            AddMedicineCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddMedicineList add = new AddMedicineList();
                add.ShowDialog();

            }
            );
            ModifyMedicineCommand = new RelayCommand<object>((p) => SelectedItemCommand != null, (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một thuốc để sửa.");
                    return;
                }

                ModifyMedicine modifyWindow = new ModifyMedicine();
                Messenger.Default.Send(SelectedItemCommand); // Gửi bệnh nhân đã chọn
                modifyWindow.ShowDialog();

            });
        }
    }
}
