using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLPhongMachTuWPF.View;


namespace QLPhongMachTuWPF.ViewModel
{
    public class PatientsVM: ViewModelBase
    {
        public ObservableCollection<Patient> PatientsList { get; set; }

        public PatientsVM()
        {
            // Khởi tạo danh sách bệnh nhân
            PatientsList = new ObservableCollection<Patient>
        {
            new Patient { PatientID = "P001", Name = "John Doe", DOB = "01/01/1990", Address = "123 Main Stasfaaaaaaaaaaaaaaaaaaaaaaaa", Phone = "1234567890", Gender = "Male" },
            new Patient { PatientID = "P002", Name = "Jane Smith", DOB = "05/05/1995", Address = "456 Oak St", Phone = "9876543210", Gender = "Male" },

        };
            
        }

    }

    public class Patient
    {
        public string PatientID { get; set; }
        public string Name { get; set;}
        public string DOB { get; set;}
        public string Phone {  get; set;}
        public string Address { get; set;}
        public string Gender { get; set;}

    }

    
}
