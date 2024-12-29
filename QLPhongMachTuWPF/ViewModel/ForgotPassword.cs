using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ForgotPassword : ViewModelBase
    {
        private string _email {  get; set; }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); }  }

        private string _username { get; set; }
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        public ICommand ResetPassword {  get; set; }

    }
}
