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
            
        }
        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            if (isSidebarCollapsed)
            {
                // Mở rộng Sidebar
                SidebarColumn.Width = new GridLength(200);
                text1.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Visible;
                logout.Visibility = Visibility.Visible;
                management.IsEnabled = true;
                medicine.IsEnabled = true;
                img.Visibility = Visibility.Visible;

            }
            else
            {
                // Thu nhỏ Sidebar
                SidebarColumn.Width = new GridLength(60);
                text1.Visibility = Visibility.Collapsed;
                text2.Visibility = Visibility.Collapsed;
                logout.Visibility = Visibility.Collapsed;
                management.IsEnabled = false;
                medicine.IsEnabled = false;
                img.Visibility = Visibility.Collapsed;
            }

            isSidebarCollapsed = !isSidebarCollapsed; // Cập nhật trạng thái
        }
    }
}
