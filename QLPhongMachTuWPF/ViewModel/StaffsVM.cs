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
  
    public class StaffsVM : ViewModelBase
    { 
        public ICommand AddStaffCommand { get; set ; }
        private ObservableCollection<NHANVIEN> _staff;
        
        private string _SearchKeyword {  get; set; }

        public string SearchKeyword { get => _SearchKeyword; 
            set
            {
                _SearchKeyword = value;
                OnPropertyChanged ();
                FilterStaffs ();
            }
        }

        public ICollectionView FilteredStaffs { get; set; }
        public ObservableCollection<NHANVIEN> StaffList { get => _staff; set { _staff = value; OnPropertyChanged(); } }
        public StaffsVM()
        {
            StaffList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs);

            FilteredStaffs = CollectionViewSource.GetDefaultView(_staff);

            Messenger.Default.Register<NHANVIEN>(this, (newStaff) =>
            {
                StaffList.Add(newStaff);
            });

            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff();
                add.ShowDialog(); 
            }
            ); 
        }
        void FilterStaffs()
        {
            if (FilteredStaffs == null)
                return;

            // Gán bộ lọc
            FilteredStaffs.Filter = (obj) =>
            {
                if (obj is NHANVIEN staffs)
                {
                    // Kiểm tra từ khóa tìm kiếm, không phân biệt chữ hoa/chữ thường
                    return string.IsNullOrEmpty(SearchKeyword) ||
                           staffs.TenNV?.IndexOf(SearchKeyword, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };

            // Làm mới view để cập nhật kết quả lọc
            FilteredStaffs.Refresh();
        }
    }
}
