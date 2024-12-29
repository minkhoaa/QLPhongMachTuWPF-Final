using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ForgotPassword : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _otp;
        public string Otp
        {
            get => _otp;
            set { _otp = value; OnPropertyChanged(); }
        }

        public ICommand SendOtpCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<ACCOUNT> listAccounts { get; set; }

        public ForgotPassword()
        {
            // Load accounts from database
            listAccounts = new ObservableCollection<ACCOUNT>(DataProvider.Ins.db.ACCOUNTs.ToList());

            // Command to send OTP
            SendOtpCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                var account = listAccounts.FirstOrDefault(x => x.UserName == Username && x.Email == Email);
                if (account != null)
                {
                    // Generate and send OTP
                    Otp = GenerateRandomCode();
                    SendOTP(Email, Otp);
                    Conformation conformation = new Conformation();
                    Messenger.Default.Send(Otp, "ConfirmOtp");
                    Messenger.Default.Send(Username, "Username");
                    p.Hide(); 
                    conformation.ShowDialog();
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or email.");
                }
            });

            // Command to close the form
            CloseCommand = new RelayCommand<Window>((p) => true, (p) => p.Close());
        }

        private static string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(10000, 100000).ToString(); // Generate a 5-digit random code
        }

        private static void SendOTP(string email, string otp)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("23520757@gm.uit.edu.vn", "hcgh hkuc eybo hxep")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("23520757@gm.uit.edu.vn"),
                    Subject = "Reset Password",
                    Body = $"Your OTP to reset password is: {otp}"
                };

                mailMessage.To.Add(email);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send OTP: {ex.Message}");
            }
        }
    }
}
