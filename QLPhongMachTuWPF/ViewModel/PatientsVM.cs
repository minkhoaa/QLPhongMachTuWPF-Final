using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;


namespace QLPhongMachTuWPF.ViewModel
{
    public class PatientsVM: ViewModelBase
    {
        private ObservableCollection<BENHNHAN> _patients;
        public ObservableCollection<BENHNHAN> PatientsList { get => _patients; set { _patients = value; OnPropertyChanged(); } }
        public ICommand CommandDeletePatients { get; set; }

        public PatientsVM()
        {
            // Khởi tạo danh sách bệnh nhân
            PatientsList =new ObservableCollection<BENHNHAN>(DataProvider.Ins.db.BENHNHANs); 
            CommandDeletePatients = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddPatient addPatientWindow = new AddPatient();
                addPatientWindow.ShowDialog();
            });
            
        }

    }

  
}
