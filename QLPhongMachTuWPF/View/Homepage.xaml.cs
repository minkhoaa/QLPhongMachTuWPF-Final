using QLPhongMachTuWPF.ViewModel;
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
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : UserControl
    {
        public Homepage()
        {
            InitializeComponent();
            this.DataContext = new HomepageVM();
            var patients = new List<Patient>
            {
                new Patient { ID = 1, DateIn = "8/16/21", Name = "Jane Cooper", DOB = 60,  Gender = "Male" },
                new Patient { ID = 2, DateIn = "9/23/21", Name = "Esther Howard", DOB = 24,  Gender = "Female" },
                new Patient { ID = 3, DateIn = "1/31/21", Name = "Guy Hawkins", DOB = 30, Gender = "Male" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
                new Patient { ID = 4, DateIn = "11/7/21", Name = "Robert Fox", DOB = 71,  Gender = "Female" },
            };

            Patientdatagrid.ItemsSource = patients;
        }
    }

    public class Patient
    {
        public int ID { get; set; }
        public string DateIn { get; set; }
        public string Name { get; set; }
        public int DOB { get; set; }
        public string Gender { get; set; }
    }

    public class Appointment
    {
        public string Time { get; set; }
        public string PatientName { get; set; }
    }
}
