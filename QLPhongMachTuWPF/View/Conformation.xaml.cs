using System;
using System.Windows;
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
    }
}
