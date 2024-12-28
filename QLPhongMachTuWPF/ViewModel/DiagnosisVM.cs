using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class DiagnosisVM : ViewModelBase
    {
        private PHIEUKHAM _SelectedItemCommand { get; set; }
        public PHIEUKHAM SelectedItemCommand { get => _SelectedItemCommand; set { _SelectedItemCommand = value; OnPropertyChanged(); } }
        public ICommand AddDiagnosisCommand { get; set; }
        public ICommand ModifyDiagnosis { get; set; }

        public ICommand DeleteDiagnosisCommand { get; set; }


        private DateTime? _FilterDateFrom = new DateTime(1990, 1, 1);
        public DateTime? FilterDateFrom
        {
            get => _FilterDateFrom;
            set
            {
                _FilterDateFrom = value;
                OnPropertyChanged();
                FilterDate();
            }
        }

        private DateTime? _FilterDateTo = new DateTime(2030,1,1);
        public DateTime? FilterDateTo
        {
            get => _FilterDateTo;
            set
            {
                _FilterDateTo = value;
                OnPropertyChanged();
                FilterDate();
            }
        }

        public ICommand VerifyCommand { get; set; }

        private ObservableCollection<PHIEUKHAM> _DiagnosisList;
        public ICollectionView FilteredDiagnosis { get; set; }
        public ObservableCollection<PHIEUKHAM> DiagnosisList { get => _DiagnosisList; set { _DiagnosisList = value; OnPropertyChanged(); } }
        public DiagnosisVM()
        {
            DiagnosisList = new ObservableCollection<PHIEUKHAM>(DataProvider.Ins.db.PHIEUKHAMs);
            FilteredDiagnosis = CollectionViewSource.GetDefaultView(DiagnosisList);

            try { 
            Messenger.Default.Register<PHIEUKHAM>(this, (diagnosis) =>
            { 
                

                if (diagnosis == null) return;

                var existingDiagnosis = DiagnosisList.FirstOrDefault(p => (p.MaPK == diagnosis.MaPK));
                if (existingDiagnosis != null)
                {
                    existingDiagnosis.TrieuChung = diagnosis.TrieuChung;
                    existingDiagnosis.KetQua = diagnosis.KetQua;
                    existingDiagnosis.TrangThai = diagnosis.TrangThai;
                    existingDiagnosis.NgayKham = diagnosis.NgayKham;
                    existingDiagnosis.MaNV = diagnosis.MaNV;
                    existingDiagnosis.MaBN = diagnosis.MaBN;
                }
                else
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DiagnosisList.Add(diagnosis);

                    });
                }

                DataProvider.Ins.db.SaveChanges();
                FilteredDiagnosis?.Refresh();


            });


            AddDiagnosisCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddMedicineDiagnosis add = new AddMedicineDiagnosis();
                add.ShowDialog();
            }
            );
            ModifyDiagnosis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ModifyDiagnosis diagnosis = new ModifyDiagnosis();
                Messenger.Default.Send(SelectedItemCommand);
                diagnosis.ShowDialog();
            }
            );
            VerifyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
              
                ModifyDetailInvoice diagnosis = new ModifyDetailInvoice();
                Messenger.Default.Send(SelectedItemCommand.HOADONs.First()); 
                diagnosis.ShowDialog();
            }
           );
            DeleteDiagnosisCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
            if (SelectedItemCommand == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu khám để xóa.");
                return;
            }
         

            // Tìm kiếm lịch hẹn liên quan đến phiếu khám
           foreach (var i in DataProvider.Ins.db.LICHHENs)
                {
                    if (i.MaPK == SelectedItemCommand.MaPK)
                    {
                        DataProvider.Ins.db.LICHHENs.Remove(i);
                    }
                }
                foreach (var item in DataProvider.Ins.db.HOADONs)
                {
                    if (item.MaPK == SelectedItemCommand.MaPK) DataProvider.Ins.db.HOADONs.Remove(item);
                }

                foreach (var i in DataProvider.Ins.db.CTTTs)
                {
                if (i.MaPK == SelectedItemCommand.MaPK)
                    {
                        DataProvider.Ins.db.CTTTs.Remove(i);
                       
                    }
                    }
                DataProvider.Ins.db.SaveChanges();
                DataProvider.Ins.db.PHIEUKHAMs.Remove(SelectedItemCommand);

                try
                {
                    DataProvider.Ins.db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    // Cập nhật danh sách `PHIEUKHAM`
                    DiagnosisList.Remove(SelectedItemCommand);

                    // Làm mới danh sách `LICHHEN`
                    Messenger.Default.Send("Refresh", "RefreshMedicineList");
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");

                    MessageBox.Show("Xóa thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message.ToString());
                }
            });
            Messenger.Default.Register<string>(this, "RefreshDiagnosisList", (message) =>
            {
                if (message == "Refresh")
                {
                    RefreshDiagnosisList();
                }
            });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public void RefreshDiagnosisList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DiagnosisList.Clear(); // Xóa danh sách cũ
                var appointments = DataProvider.Ins.db.PHIEUKHAMs.ToList(); // Lấy danh sách mới từ DB
                foreach (var appointment in appointments)
                {
                    DiagnosisList.Add(appointment);
                }
            });
        }
        public void FilterDate()
        {
       

            if (FilterDateFrom != null && FilterDateTo != null)
            {
                FilteredDiagnosis.Filter = (item) =>
                {
                    var diagnosis = item as PHIEUKHAM;
                    if (diagnosis == null || diagnosis.NgayKham == null) return false;

                    // So sánh ngày khám với khoảng thời gian được lọc
                    return diagnosis.NgayKham >= FilterDateFrom && diagnosis.NgayKham <= FilterDateTo;
                };
                FilteredDiagnosis.Refresh();
            }
            else
            {
                // Nếu không có ngày lọc, loại bỏ bộ lọc
                
                FilteredDiagnosis.Refresh();
            }
        }

    }
}
