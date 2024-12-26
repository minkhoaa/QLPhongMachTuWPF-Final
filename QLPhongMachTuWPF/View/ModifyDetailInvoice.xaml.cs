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

namespace QLPhongMachTuWPF.View
{
    /// <summary>
    /// Interaction logic for ModifyDetailInvoice.xaml
    /// </summary>
    public partial class ModifyDetailInvoice : Window
    {
        public ModifyDetailInvoice()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)

        {
            this.Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public class InvoiceItem
        {
            public string ID { get; set; }
            public string Medicine { get; set; }

            public string Price { get; set; }

            public string Quantity { get; set; }
            public string Total { get; set; }

        }
    }
}
