using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : UserControl
    {
        public Patients()
        {
            InitializeComponent();
            ObservableCollection<PatientsSource> patients = new ObservableCollection<PatientsSource>();
            patients.Add(new PatientsSource { PatientID = "9012401", PatientName = "Tran Dang Khoa", DOB = "27/10/2005", PatientAddress = "Quan 1 ahflkajlkfjalkfj", PatientPhone = "123155151", Gender = "Male", Status = "Stable" });
            patients.Add(new PatientsSource { PatientID = "9012401", PatientName = "Tu Minh Khoa", DOB = "27/10/2005", PatientAddress = "Quan 1", PatientPhone = "123155151", Gender = "Male", Status = "Stable" });
            patientsDatagrid.ItemsSource = patients;
        }
        public class PatientsSource
        {
            public string PatientID { get; set; }
            public string PatientName { get; set; }
            public string DOB { get; set; }
            public string PatientAddress { get; set; }
            public string PatientPhone { get; set; }
            public string Gender { get; set; }
            public string Status { get; set; }
        }
        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ AddPatientWindow
            AddPatient addPatientWindow = new AddPatient();
            addPatientWindow.ShowDialog(); 
        }
    }
}
