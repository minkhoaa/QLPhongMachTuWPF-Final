﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace QLPhongMachTuWPF.ViewModel
{
    public class Navigation : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

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

        public Navigation()
        {
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
