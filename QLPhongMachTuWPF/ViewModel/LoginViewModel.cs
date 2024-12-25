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
namespace QLPhongMachTuWPF.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public bool isLogin { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand EnterKeyCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private string _username;

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }


        private string _password;

        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public LoginViewModel()
        {
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
            var count = DataProvider.Ins.db.ACCOUNTs
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
        public static string HashPasswordWithSHA256(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    } 
}
