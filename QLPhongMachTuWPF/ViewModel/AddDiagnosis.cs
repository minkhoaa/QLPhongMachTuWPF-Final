using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddDiagnosis : ViewModelBase
    {
        private string _Name { get; set; }

        public ICommand AddDiagnosisCommand { get; set; }

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        public AddDiagnosis()
        {
            AddDiagnosisCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name)) return false;
                return true;
            }, (p) => {
                //  var normalizedNgaySinh = NormalizeDateTime(NgaySinh); // Chuẩn hóa giá trị

                var newDiagnosis = new PHIEUKHAM()
                {

                };

                try
                {
                    // Lưu vào cơ sở dữ liệu
                    DataProvider.Ins.db.PHIEUKHAMs.Add(newDiagnosis);
                    DataProvider.Ins.db.SaveChanges();

                    // Gửi thông báo kèm bệnh nhân mới
                    Messenger.Default.Send(newDiagnosis);

                    MessageBox.Show("Thêm bệnh nhân thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm bệnh nhân: {ex.Message}");
                }
            });


        }
    }

}