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

namespace QLPhongMachTuWPF.ViewModel
{
  
    public class StaffsVM : ViewModelBase
    { 
        public ICommand AddStaffCommand { get; set ; }
        private ObservableCollection<NHANVIEN> _staff;
        public ObservableCollection<NHANVIEN> StaffList { get => _staff; set { _staff = value; OnPropertyChanged(); } }
        public StaffsVM()
        {
            StaffList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs);
            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff(); 
                add.ShowDialog();

            }
            ); 
        }
    }
}
