using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;


namespace QLPhongMachTuWPF.ViewModel
{
    public class PatientsVM: ViewModelBase
    {
        private ObservableCollection<BENHNHAN> _patients;
        public ObservableCollection<BENHNHAN> PatientsList { get => _patients; set { _patients = value; OnPropertyChanged();  } }

       
        private BENHNHAN _SelectedItemCommand { get; set; }

        public BENHNHAN SelectedItemCommand {  
                get => _SelectedItemCommand;
                set
    {
                    if (_SelectedItemCommand != value)
                    {
                        _SelectedItemCommand = value;
                        OnPropertyChanged();
                }
                }
            } 

        #region SendPatient
        private string _TenBN { get; set; }
        public string TenBN { get => _TenBN; set { _TenBN = value; OnPropertyChanged(); } }

        private string _DiaChi { get; set; }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }


        private string _DienThoai { get; set; }

        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }

        private DateTime _NgaySinh { get; set; }
        public DateTime NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }

        private string _Gender { get; set; }
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Charged { get; set; }
        public string Charged { get => _Charged; set { _Charged = value; OnPropertyChanged(); } }

        #endregion
     

        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
                FilterPatients(); // Gọi hàm lọc mỗi khi từ khóa thay đổi
            }
        }
        public ICommand AddPatientCommand { get; set; }

        public ICommand ModifyPatientCommand { get; set; }

        public ICommand DeletePatientCommand { get; set; }

        
        public ICollectionView FilteredPatients { get; set; }

        public PatientsVM()
        {
            // Khởi tạo danh sách bệnh nhân
            PatientsList = new ObservableCollection<BENHNHAN>(DataProvider.Ins.db.BENHNHANs);

            // Khởi tạo bộ lọc
            FilteredPatients = CollectionViewSource.GetDefaultView(PatientsList);

            // Đăng ký nhận thông báo từ Messenger
            Messenger.Default.Register<BENHNHAN>(this, (patient) =>
            {
                if (patient == null) return;

                // Kiểm tra nếu bệnh nhân đã tồn tại
                var existingPatient = PatientsList.FirstOrDefault(p => p.MaBN == patient.MaBN);
                if (existingPatient != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Cập nhật thông tin bệnh nhân
                        existingPatient.TenBN = patient.TenBN;
                        existingPatient.DiaChi = patient.DiaChi;
                        existingPatient.GioiTinh = patient.GioiTinh;
                        existingPatient.DienThoai = patient.DienThoai;
                        existingPatient.NgaySinh = patient.NgaySinh;
                        existingPatient.TrangThai = patient.TrangThai;
                     
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Thêm bệnh nhân mới
                        PatientsList.Add(patient);
                      
                    });
                }

                // Làm mới view
                DataProvider.Ins.db.SaveChanges();
                FilteredPatients?.Refresh();
            });

            // Khởi tạo lệnh
            AddPatientCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                AddPatient addPatientWindow = new AddPatient();
                addPatientWindow.ShowDialog();
            });

            ModifyPatientCommand = new RelayCommand<object>((p) => SelectedItemCommand != null, (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân để sửa.");
                    return;
                }

                ModifyPatients modifyWindow = new ModifyPatients();
                Messenger.Default.Send(SelectedItemCommand); // Gửi bệnh nhân đã chọn
                modifyWindow.ShowDialog();

            });
        }
        private void FilterPatients()
        {
            if (FilteredPatients == null)
                return;

            // Gán bộ lọc
            FilteredPatients.Filter = (obj) =>
            {
                if (obj is BENHNHAN patient)
                {
                    // Kiểm tra từ khóa tìm kiếm, không phân biệt chữ hoa/chữ thường
                    return string.IsNullOrEmpty(SearchKeyword) ||
                           patient.TenBN?.IndexOf(SearchKeyword, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };

            // Làm mới view để cập nhật kết quả lọc
            FilteredPatients.Refresh();
        }

    }
}
