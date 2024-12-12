using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ObservableCollection<BENHNHAN> PatientsList { get => _patients; set { _patients = value; OnPropertyChanged(); } }

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
        public ICollectionView FilteredPatients { get; set; }

        public PatientsVM()
        {
            // Khởi tạo danh sách bệnh nhân
            PatientsList = new ObservableCollection<BENHNHAN>(DataProvider.Ins.db.BENHNHANs);

            // Đăng ký nhận thông báo
            FilteredPatients = CollectionViewSource.GetDefaultView(_patients);
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
