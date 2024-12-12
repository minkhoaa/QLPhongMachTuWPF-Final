using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddInvoiceVM : ViewModelBase
    {   
        public ICommand addCommand { get ; set; }
        public AddInvoiceVM() {
            addCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                
                MessageBox.Show("thêm chức năng button này"); 
            });


        }
    }
}
