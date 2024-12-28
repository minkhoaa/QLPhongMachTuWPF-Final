using QLPhongMachTuWPF.View;
using QLPhongMachTuWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using QLPhongMachTuWPF.Model;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Security.Principal;
using iText.Layout.Properties.Grid;


namespace QLPhongMachTuWPF.ViewModel

{
    public class LoginViewModel : ViewModelBase
    {

       
        public bool isLogin { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand EnterKeyCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private string _username;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        private string _type;
        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }
        public ObservableCollection<ACCOUNT> listAccounts { get; set; }

        

        public LoginViewModel()
        {
            listAccounts = new ObservableCollection<ACCOUNT>(DataProvider.Ins.db.ACCOUNTs.ToList());
            isLogin = false;

            // Đăng ký các lệnh
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });

            // Đổi mật khẩu
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Register register = new Register();
                register.ShowDialog();
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Application.Current.Shutdown();
            });
        }

        void Login(Window p)
        {
            if (p == null) return;
            string hashedPassword = HashPasswordWithSHA256(Password);
            var account = listAccounts
                .FirstOrDefault(x => x.UserName == Username && x.PassWord == hashedPassword);

            if (account != null)
            {
                isLogin = true;
                Type = account.Type.ToString();

                // Send account info to MainWindow
                Messenger.Default.Send(new AccountType
                {
                    UserName = account.UserName,
                    Type = account.Type
                });

                p.Close();
            }
            else
            {
                isLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }
       
    }

}
