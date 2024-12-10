using MaterialDesignThemes.Wpf;
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

   
        public class InvoiceVM : ViewModelBase
        {
            public ICommand AddStaffCommand { get; set; }
            private ObservableCollection<HOADON> _invoice;
            public ObservableCollection<HOADON> InvoiceList { get => _invoice; set { _invoice = value; OnPropertyChanged(); } }
            public InvoiceVM()
            {
                InvoiceList = new ObservableCollection<HOADON>(DataProvider.Ins.db.HOADONs );
                AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    AddStaff add = new AddStaff();
                    add.ShowDialog();

                }
                );
            }
        }
    
}

