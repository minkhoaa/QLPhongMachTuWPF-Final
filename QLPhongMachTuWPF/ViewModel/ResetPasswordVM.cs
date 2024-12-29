using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Security.Permissions;
using QLPhongMachTuWPF.Model;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ResetPasswordVM : ViewModelBase
    {

        private string _password {  get; set; }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }
        private string _username { get; set; }
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }
        private string _confirmpassword { get; set; }
        public string ConfirmPassword
        {
            get => _confirmpassword;
            set { _confirmpassword = value; OnPropertyChanged(); }
        }
        public ICommand CloseCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        public ResetPasswordVM()
        {
            Messenger.Default.Register<string>(this, "Username", (message) =>
            {
                Username = message;
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            DoneCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp");
                }
                else
                {
                    var account = DataProvider.Ins.db.ACCOUNTs.FirstOrDefault(x => x.UserName == Username);
                    if (account == null) { MessageBox.Show("Tài khoản không tồn tại"); }
                    else
                    {
                        account.PassWord = HashPasswordWithSHA256(Password);
                        DataProvider.Ins.db.SaveChanges();
                        Messenger.Default.Send("Refresh", "LoadAccountList");

                        MessageBox.Show("Thay đổi thành công");
                     
                        p.Close();
                    }
                }
            });

        } // Messenger.Default.Send("Refresh", "RefreshAppointmentList");
        //
    }

    //Messenger.Default.Register<string>(this, "RefreshAppointmentList", (message) =>
    //        {
    //            if (message == "Refresh")
    //            {
    //    RefreshAppointmentList();
    //}

    //});
}
