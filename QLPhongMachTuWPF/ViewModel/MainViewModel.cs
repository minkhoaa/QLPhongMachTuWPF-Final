using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; 

namespace QLPhongMachTuWPF.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand HomePageCommand { get; set; }
        public ICommand PatientsCommand { get; set; }
        public ICommand StaffsCommand { get; set; }


        public ICommand ManageAppointmentCommand { get; set; }
        public ICommand DiagnosisCommand { get; set; }
        public ICommand ListsCommand { get; set; }
        public ICommand InvoiceCommand { get; set; }


        private void HomePage(object obj) => CurrentView = new HomepageVM();
        private void Patients(object obj) => CurrentView = new PatientsVM();
        private void Staffs(object obj) => CurrentView = new StaffsVM();
        private void ManageAppointment(object obj) => CurrentView = new ManageAppointmentsVM();
        private void Diagnosis(object obj) => CurrentView = new DiagnosisVM();
        private void Lists(object obj) => CurrentView = new ListsVM();
        private void Invoice(object obj) => CurrentView = new InvoiceVM();

        public bool isLoad = false; 
 
        public MainViewModel() 
        {
            
           
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                isLoad = true;

                if (p == null) return; 

                p.Hide(); 

                LoginModel login = new LoginModel();
                login.ShowDialog();
                if (login.DataContext == null) return;
                var isLogined = login.DataContext as LoginViewModel;
                if (isLogined.isLogin)
                {

                    p.Show(); 
                }
                else {
                    p.Close();
                }
            }
            );
            HomePageCommand = new RelayCommand(HomePage);
            PatientsCommand = new RelayCommand(Patients);
            StaffsCommand = new RelayCommand(Staffs);
            ManageAppointmentCommand = new RelayCommand(ManageAppointment);
            DiagnosisCommand = new RelayCommand(Diagnosis);
            ListsCommand = new RelayCommand(Lists);
            InvoiceCommand = new RelayCommand(Invoice);

            CurrentView = new HomepageVM();
        }
    }
}
