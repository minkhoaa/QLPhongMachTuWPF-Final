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

       public ObservableCollection<ACCOUNT> listAccounts { get; set; } 

        public LoginViewModel()
        {
            listAccounts = new ObservableCollection<ACCOUNT>(DataProvider.Ins.db.ACCOUNTs.ToList()); 
            
            isLogin = false;

            /*  admin admin
             *  employer employer
             *  
             */
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);

            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
               
                Register register = new Register();
                register.ShowDialog();
              
            });
              LoginCommand = new RelayCommand<Window>((p) => { if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return false; return true; ; }, (p) =>
            {
                Login(p);

            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Application.Current.Shutdown();

            });
                Messenger.Default.Register<string>(this, "RefreshAccountList", (message) =>
                {
                    if (message == "Refresh")
                    {
                        RefreshAccountList();
                    }

                });
           

         
        }


        private void OnEnterKeyPressed()
        {
            // Kiểm tra điều kiện và gọi lệnh LoginCommand khi Enter được nhấn
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                // Gọi LoginCommand
                Login(null);
            }
        }
        void Login(Window p)
        {
            if (p == null) return;
            string hashedPassword = HashPasswordWithSHA256(Password);
            var count = listAccounts
      .Where(x => x.UserName == Username && x.PassWord == hashedPassword)
      .Count();


            if (count > 0)
            {
                isLogin = true;
                p.Close();
            }
            else
            {
                isLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }

        }


        public void RefreshAccountList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                listAccounts.Clear(); // Xóa danh sách cũ
                var Medicine = new ObservableCollection<ACCOUNT>(DataProvider.Ins.db.ACCOUNTs.ToList()); // Lấy danh sách mới từ DB
                foreach (var item in Medicine)
                {
                    listAccounts.Add(item);
                }
            });
        }

    } 
}
