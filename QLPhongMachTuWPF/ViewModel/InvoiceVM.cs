using MaterialDesignThemes.Wpf;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{

    public class InvoiceVM : ViewModelBase
    {
        public ICommand AddInvoiceCommand { get; set; }

        public InvoiceVM()
        {

            AddInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("thêm page addInvoice"); 
            }
            );


        }
    }
}

