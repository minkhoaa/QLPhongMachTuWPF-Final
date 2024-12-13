using QLPhongMachTuWPF.View;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QLPhongMachTuWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentView;

        // Các thuộc tính Background cho các nút
        private Brush _patientBackground = Brushes.Transparent;
        private Brush _staffBackground = Brushes.Transparent;
        private Brush _diagnosisBackground = Brushes.Transparent;
        private Brush _listsBackground = Brushes.Transparent;
        private Brush _appointmentBackground = Brushes.Transparent;
        private Brush _invoiceBackground = Brushes.Transparent;

        // Thuộc tính màu nền cho Home
        private Brush _homeBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); // #ccc mặc định

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Các thuộc tính Background
        public Brush PatientBackground
        {
            get => _patientBackground;
            set { _patientBackground = value; OnPropertyChanged(); }
        }

        public Brush StaffBackground
        {
            get => _staffBackground;
            set { _staffBackground = value; OnPropertyChanged(); }
        }

        public Brush DiagnosisBackground
        {
            get => _diagnosisBackground;
            set { _diagnosisBackground = value; OnPropertyChanged(); }
        }

        public Brush ListsBackground
        {
            get => _listsBackground;
            set { _listsBackground = value; OnPropertyChanged(); }
        }

        public Brush AppointmentBackground
        {
            get => _appointmentBackground;
            set { _appointmentBackground = value; OnPropertyChanged(); }
        }

        public Brush InvoiceBackground
        {
            get => _invoiceBackground;
            set { _invoiceBackground = value; OnPropertyChanged(); }
        }

        public Brush HomeBackground
        {
            get => _homeBackground;
            set { _homeBackground = value; OnPropertyChanged(); }
        }

        // Các Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand HomePageCommand { get; set; }
        public ICommand PatientsCommand { get; set; }
        public ICommand StaffsCommand { get; set; }
        public ICommand ManageAppointmentCommand { get; set; }
        public ICommand DiagnosisCommand { get; set; }
        public ICommand ListsCommand { get; set; }
        public ICommand InvoiceCommand { get; set; }

        // Các hàm Command
        private void HomePage(object obj)
        {
            CurrentView = new HomepageVM();

            // Cập nhật màu nền khi chọn Home
            HomeBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset màu
            PatientBackground = Brushes.Transparent; 
            StaffBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent; 
            ListsBackground = Brushes.Transparent; 
            AppointmentBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
        }

        private void Patients(object obj)
        {
            CurrentView = new PatientsVM();
            // Cập nhật màu nền khi chọn Patients
            PatientBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            //Reset
            StaffBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent; 
            ListsBackground = Brushes.Transparent;
            AppointmentBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
            HomeBackground=Brushes.Transparent;
        }

        private void Staffs(object obj)
        {
            CurrentView = new StaffsVM();
            // Cập nhật màu nền khi chọn Staffs
            StaffBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset
            PatientBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent;
            ListsBackground = Brushes.Transparent; 
            AppointmentBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
            HomeBackground = Brushes.Transparent;
        }

        private void Diagnosis(object obj)
        {
            CurrentView = new DiagnosisVM();
            // Cập nhật màu nền khi chọn Diagnosis
            DiagnosisBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset
            PatientBackground = Brushes.Transparent; 
            StaffBackground = Brushes.Transparent; 
            ListsBackground = Brushes.Transparent; 
            AppointmentBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
            HomeBackground = Brushes.Transparent;
        }

        private void Lists(object obj)
        {
            CurrentView = new ListsVM();
            // Cập nhật màu nền khi chọn Lists
            ListsBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset màu
            PatientBackground = Brushes.Transparent; 
            StaffBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent; 
            AppointmentBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
            HomeBackground = Brushes.Transparent;
        }

        private void ManageAppointment(object obj)
        {
            CurrentView = new ManageAppointmentsVM();
            // Cập nhật màu nền 
            AppointmentBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset màu
            PatientBackground = Brushes.Transparent; 
            StaffBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent; 
            ListsBackground = Brushes.Transparent; 
            InvoiceBackground = Brushes.Transparent; 
            HomeBackground = Brushes.Transparent;
        }

        private void Invoice(object obj)
        {
            CurrentView = new InvoiceVM();
            // Cập nhật màu nền khi chọn Invoice
            InvoiceBackground = new SolidColorBrush(Color.FromRgb(204, 204, 204)); 
            //Reset màu
            PatientBackground = Brushes.Transparent; 
            StaffBackground = Brushes.Transparent; 
            DiagnosisBackground = Brushes.Transparent; 
            ListsBackground = Brushes.Transparent; 
            AppointmentBackground = Brushes.Transparent; 
            HomeBackground = Brushes.Transparent;
        }

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
                else
                {
                    p.Close();
                }
            });

            // Các Command khác
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
