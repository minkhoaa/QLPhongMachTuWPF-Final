using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QLPhongMachTuWPF.View;

namespace QLPhongMachTuWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSidebarCollapsed = false;
        public MainWindow()
        {

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;


        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_patients.Visibility = Visibility.Collapsed;
                tt_staffs.Visibility = Visibility.Collapsed;
                tt_appoinntments.Visibility = Visibility.Collapsed;
                tt_diagnosis.Visibility = Visibility.Collapsed;
                tt_lists.Visibility = Visibility.Collapsed;
                tt_invoice.Visibility = Visibility.Collapsed;
                tt_logout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_patients.Visibility = Visibility.Visible;
                tt_staffs.Visibility = Visibility.Visible;
                tt_appoinntments.Visibility = Visibility.Visible;
                tt_diagnosis.Visibility = Visibility.Visible;
                tt_lists.Visibility = Visibility.Visible;
                tt_invoice.Visibility = Visibility.Visible;
                tt_logout.Visibility = Visibility.Visible;
            }
        }
        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
         
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.None;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;

             
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                var storyboard = new Storyboard();
                storyboard.Children.Add(animation);
                Storyboard.SetTarget(animation, this);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                storyboard.Begin();
            }
            else
            {
           
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;

               
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                var storyboard = new Storyboard();
                storyboard.Children.Add(animation);
                Storyboard.SetTarget(animation, this);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                storyboard.Begin();
            }
        }

        

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }

}
