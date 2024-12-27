using QLPhongMachTuWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class RegisterVM : ViewModelBase
    {
        private string _displayname;
        public ICommand RegisterCommand { get; set; }

        public ICommand PasswordChangedCommand { get; set; }
        public ICommand PasswordConfirmChangedCommand { get; set; }
        public string Displayname { get => _displayname; set { _displayname = value; OnPropertyChanged(); } }
        private string _username;

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }


        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        private string _confirmpassword;
        public string Confirmpassword { get => _confirmpassword; set { _confirmpassword = value; OnPropertyChanged(); } }
        public RegisterVM()
        {
            RegisterCommand = new RelayCommand<Window>((p) =>
            {
                // Điều kiện để cho phép thực hiện lệnh
                return !string.IsNullOrEmpty(Username) &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(Confirmpassword) &&
                       Password == Confirmpassword &&
                       !string.IsNullOrEmpty(Displayname);
            },
 (p) =>
 {
     if (Register(p))
     {
         MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

         // Đóng form đăng ký
         p.Close();

         // Hiển thị lại form đăng nhập
         OpenLoginForm();
     }
     else
     {
         MessageBox.Show("Đăng ký không thành công. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
     }
 });


            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            PasswordConfirmChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                // Điều kiện để kích hoạt lệnh, nếu cần
                return p != null;
            },
   (p) =>
   {
       // Cập nhật giá trị ConfirmPassword từ PasswordBox
       Confirmpassword = p.Password;

       // Kiểm tra nếu cần
       
   });
        }
        private void OpenLoginForm()
        {
            // Giả sử bạn có một form đăng nhập tên là LoginWindow
            LoginModel loginWindow = new LoginModel();
            loginWindow.Show();
        }

        bool Register(Window p)
        {
            if (p == null) return false;

            // Kiểm tra điều kiện
            if (string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(Confirmpassword) ||
                Password != Confirmpassword ||
                string.IsNullOrEmpty(Displayname))
            {
                return false;
            }

            // Kiểm tra xem tên người dùng đã tồn tại hay chưa
            var existingUser = DataProvider.Ins.db.ACCOUNTs.FirstOrDefault(x => x.UserName == Username);
            if (existingUser != null)
            {
               
                return false;
            }

            // Thêm tài khoản mới
            DataProvider.Ins.db.ACCOUNTs.Add(new ACCOUNT()
            {
                DisPlayName = Displayname,
                UserName = Username,
                PassWord = HashPasswordWithSHA256(Password),
                Type = 1 // Đặt mặc định Type là 1 hoặc tùy chỉnh theo logic của bạn
            });

            // Lưu thay đổi
            DataProvider.Ins.db.SaveChanges();

            return true;
        }

    }
}
