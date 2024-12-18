using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddDiagnosis : ViewModelBase
    {
        private string _Name;
        private string _Symptoms;
        private string _Result;
        private DateTime _DiagnosisDate;
        private int _PatientId;
        private int _StaffId;

        public ICommand AddDiagnosisCommand { get; set; }

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        public string Symptoms { get => _Symptoms; set { _Symptoms = value; OnPropertyChanged(); } }
        public string Result { get => _Result; set { _Result = value; OnPropertyChanged(); } }
        public DateTime DiagnosisDate { get => _DiagnosisDate; set { _DiagnosisDate = value; OnPropertyChanged(); } }
        public int PatientId { get => _PatientId; set { _PatientId = value; OnPropertyChanged(); } }
        public int StaffId { get => _StaffId; set { _StaffId = value; OnPropertyChanged(); } }

        public AddDiagnosis()
        {
            AddDiagnosisCommand = new RelayCommand<object>((p) =>
            {
                // Điều kiện cho phép thực thi
                return !string.IsNullOrEmpty(Name) && DiagnosisDate != default(DateTime) && PatientId > 0 && StaffId > 0;
            },
            (p) =>
            {
                // Tạo đối tượng PHIEUKHAM mới
                var newDiagnosis = new PHIEUKHAM()
                {
                    MaBN = PatientId,
                    MaNV = StaffId,
                    NgayKham = DiagnosisDate,
                    TrieuChung = Symptoms,
                    KetQua = Result,
                    TrangThai = 1 // Đã khám
                };

                try
                {
                    // Lưu phiếu khám vào cơ sở dữ liệu
                    DataProvider.Ins.db.PHIEUKHAMs.Add(newDiagnosis);
                    DataProvider.Ins.db.SaveChanges();

                    // Gửi thông báo về phiếu khám mới
                    Messenger.Default.Send(newDiagnosis);

                    MessageBox.Show("Thêm phiếu khám thành công!");

                    // Reset các trường
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm phiếu khám: {ex.Message}");
                }
            });
        }

        private void ResetFields()
        {
            Name = string.Empty;
            Symptoms = string.Empty;
            Result = string.Empty;
            DiagnosisDate = default(DateTime);
            PatientId = 0;
            StaffId = 0;
        }
    }
}
