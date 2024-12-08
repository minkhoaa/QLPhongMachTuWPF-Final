using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QLPhongMachTuWPF.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        public bool isLoad = false; 
        public MainViewModel() {
            isLoad = true;
            if (isLoad == false)
            {
                LoginModel login = new LoginModel();
                login.ShowDialog();
            }
        }
    }
}
