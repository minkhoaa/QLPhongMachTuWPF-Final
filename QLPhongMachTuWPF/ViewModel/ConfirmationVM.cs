using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ConfirmationVM : ViewModelBase
    {
        private string _Otp {  get; set; }
        public string Otp
        {
            get => _Otp;
            set { _Otp = value; OnPropertyChanged(); }
        }
        private string _username { get; set; }
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }


        private string _confirmotp;
        public string ConfirmOtp
        {
            get => _confirmotp;
            set { _confirmotp = value; OnPropertyChanged(); }
        }

        public ICommand CloseCommand { get; set; }

        public ICommand VerifyCommand { get; set; }

        public ConfirmationVM()
        {
            Messenger.Default.Register<string>(this, "ConfirmOtp", (message) =>
            {
               Otp = message;
            });
            Messenger.Default.Register<string>(this, "Username", (message) =>
            {
                Username = message;
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            VerifyCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                if (Otp == ConfirmOtp)
                {
                    ResetPass resetPass = new ResetPass();
                    Messenger.Default.Send(Username, "Username");
                    p.Hide();
                    resetPass.ShowDialog();
                    p.Close(); 
                }
            });

        }
    }
}
