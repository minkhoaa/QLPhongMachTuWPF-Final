using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;


namespace QLPhongMachTuWPF.ViewModel
{
    public class PatientsVM: ViewModelBase
    {
        private ObservableCollection<BENHNHAN> _patients;
        public ObservableCollection<BENHNHAN> PatientsList { get => _patients; set { _patients = value; OnPropertyChanged(); } }
        public ICommand AddPatientCommand { get; set; }

        public PatientsVM()
        {
            // Khởi tạo danh sách bệnh nhân
            PatientsList = new ObservableCollection<BENHNHAN>(DataProvider.Ins.db.BENHNHANs);

            // Đăng ký nhận thông báo
            Messenger.Default.Register<BENHNHAN>(this, (newPatient) =>
            {
                PatientsList.Add(newPatient);
            });

            AddPatientCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddPatient addPatientWindow = new AddPatient();
                addPatientWindow.ShowDialog();
            });

          
        }

    }
}
