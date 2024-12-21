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
using System.Windows.Shapes;

namespace QLPhongMachTuWPF.View
{
    /// <summary>
    /// Interaction logic for DetailInovice.xaml
    /// </summary>
    public partial class DetailInovice : Window
    {
        public DetailInovice()
        {
            InitializeComponent();
            List<InvoiceItem> items = new List<InvoiceItem>
                {
                new InvoiceItem { ID = "01", Medicine = "Business Card Design", Price = "$40.00", Quantity = "01", Total = "$40.00" },
                new InvoiceItem { ID = "02", Medicine = "Web PSD Design", Price = "$350.00", Quantity = "02", Total = "$350.00" },
                new InvoiceItem { ID = "03", Medicine = "Letterhead INCD", Price = "$70.00", Quantity = "03", Total = "$140.00" },
                new InvoiceItem { ID = "04", Medicine = "Branding Complete", Price = "$1000.00", Quantity = "04", Total = "$1000.00" },
                new InvoiceItem { ID = "05", Medicine = "Corporate Flyer", Price = "$100.00", Quantity = "05", Total = "$300.00" },
                new InvoiceItem { ID = "06", Medicine = "Web Development", Price = "$1200.00", Quantity = "06", Total = "$1200.00" },
                new InvoiceItem { ID = "07", Medicine = "Website SEO", Price = "$550.00", Quantity = "07", Total = "$1100.00" },
            };
            datagrid.ItemsSource = items;
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
