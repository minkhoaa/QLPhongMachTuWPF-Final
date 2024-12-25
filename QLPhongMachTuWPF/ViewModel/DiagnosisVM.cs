using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
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

namespace QLPhongMachTuWPF.ViewModel
{
    public class DiagnosisVM : ViewModelBase
    {
        private PHIEUKHAM _SelectedItemCommand { get; set; }
        public PHIEUKHAM SelectedItemCommand { get => _SelectedItemCommand; set { _SelectedItemCommand = value; OnPropertyChanged(); } }
        public ICommand AddDiagnosisCommand { get; set; }
        public ICommand ModifyDiagnosis { get; set; }

        public ICommand DeleteDiagnosisCommand { get; set; }



        public ICommand VerifyCommand { get; set; }

        private ObservableCollection<PHIEUKHAM> _DiagnosisList;
        public ICollectionView FilteredDiagnosis { get; set; }
        public ObservableCollection<PHIEUKHAM> DiagnosisList { get => _DiagnosisList; set { _DiagnosisList = value; OnPropertyChanged(); } }
        public DiagnosisVM()
        {
            DiagnosisList = new ObservableCollection<PHIEUKHAM>(DataProvider.Ins.db.PHIEUKHAMs);

            FilteredDiagnosis = CollectionViewSource.GetDefaultView(DiagnosisList);
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
                //   SelectedItemCommand.NHANVIEN = DataProvider.Ins.db.NHANVIENs.Where(x => x.MaNV == SelectedItemCommand.MaNV) as NHANVIEN;
                ModifyDiagnosis diagnosis = new ModifyDiagnosis();
                Messenger.Default.Send(SelectedItemCommand);
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
                var existingAppointment = DataProvider.Ins.db.LICHHENs
                    .FirstOrDefault(x => x.NgayKham == SelectedItemCommand.NgayKham && x.TenBN == SelectedItemCommand.BENHNHAN.TenBN);

                if (existingAppointment != null)
                {
                    DataProvider.Ins.db.LICHHENs.Remove(existingAppointment);
                }

                DataProvider.Ins.db.PHIEUKHAMs.Remove(SelectedItemCommand);

                try
                {
                    DataProvider.Ins.db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    // Cập nhật danh sách `PHIEUKHAM`
                    DiagnosisList.Remove(SelectedItemCommand);

                    // Làm mới danh sách `LICHHEN`
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");


                    MessageBox.Show("Xóa thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message.ToString());
                }
            });

        }
    }
    }
