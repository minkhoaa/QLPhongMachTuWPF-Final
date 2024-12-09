using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
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
        public StaffsVM() {
            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff(); 
                add.ShowDialog();

            }
            ); 
        }
    }
}
