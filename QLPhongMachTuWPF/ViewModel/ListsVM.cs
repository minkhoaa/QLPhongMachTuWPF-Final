using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ListsVM : ViewModelBase
    {
        public ICommand AddStaffCommand { get; set; }
        private ObservableCollection<THUOC> _medicine;
        public ObservableCollection<THUOC> MedicineList { get => _medicine; set { _medicine = value; OnPropertyChanged(); } }
        public ListsVM()
        {
            MedicineList = new ObservableCollection<THUOC>(DataProvider.Ins.db.THUOCs);
            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff();
                add.ShowDialog();

            }
            );
        }
    }
}
