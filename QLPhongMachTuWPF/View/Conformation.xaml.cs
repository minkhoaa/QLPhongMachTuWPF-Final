using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace QLPhongMachTuWPF.View
{
    public partial class Conformation : Window
    {
        private DispatcherTimer timer;
        private TimeSpan time;

        public Conformation()
        {
            InitializeComponent();

            // Khởi tạo thời gian đếm ngược (5 phút)
            time = TimeSpan.FromMinutes(5);

            // Thiết lập DispatcherTimer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            UpdateTimeDisplay();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time.TotalSeconds > 0)
            {
                time = time.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimeDisplay();
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Code has expired. Please request a new code.");
            }
        }

        private void UpdateTimeDisplay()
        {
            textExpiration.Text = time.ToString(@"mm\:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

       
        

       
       

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }







        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current?.Shutdown();
        }
    }


}
