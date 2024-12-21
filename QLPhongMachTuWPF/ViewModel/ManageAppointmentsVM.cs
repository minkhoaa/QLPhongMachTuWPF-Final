using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ManageAppointmentsVM : ViewModelBase
    {
        private LICHHEN _SelectedItemCommand { get; set; }

        public LICHHEN SelectedItemCommand
        {
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

        public ICommand VerifyCommand { get; set; }
        public ICommand AddAppointmentCommand { get; set; }

        public ICommand ModifyAppointmentCommand { get; set; }

        public ICollectionView FilteredAppointment{ get; set; }

        private ObservableCollection<LICHHEN> _Appointment;
        public ObservableCollection<LICHHEN> AppointmentList { get => _Appointment; set { _Appointment = value; OnPropertyChanged(); } }

        
        public ManageAppointmentsVM()
        {

            AppointmentList = new ObservableCollection<LICHHEN>(DataProvider.Ins.db.LICHHENs);
            FilteredAppointment = CollectionViewSource.GetDefaultView(AppointmentList);
            Messenger.Default.Register<LICHHEN>(this, (Appointment) =>
            {
                if (Appointment == null) return;

                // Kiểm tra nếu bệnh nhân đã tồn tại
                var existingAppointment = AppointmentList.FirstOrDefault(p => p.MaLH == Appointment.MaLH);
                if (existingAppointment != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Cập nhật thông tin bệnh nhân
                        existingAppointment.TenBN = Appointment.TenBN;
                        existingAppointment.DiaChi = Appointment.DiaChi;
                        existingAppointment.GioiTinh = Appointment.GioiTinh;
                        existingAppointment.DienThoai = Appointment.DienThoai;
                        existingAppointment.NgaySinh = Appointment.NgaySinh;
                        existingAppointment.TrangThai = Appointment.TrangThai;
                        existingAppointment.TenNV = Appointment.TenNV;
                        existingAppointment.NgayKham = Appointment.NgayKham;

                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Thêm bệnh nhân mới
                        AppointmentList.Add(Appointment);

                    });
                }

                // Làm mới view
                DataProvider.Ins.db.SaveChanges();
                FilteredAppointment?.Refresh();
            });
          


               AddAppointmentCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddAppointment add = new AddAppointment();
                add.ShowDialog(); 
            }
            );

            VerifyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemCommand.NgaySinh.HasValue)
                {
                    DateTime dateOfBirth = SelectedItemCommand.NgaySinh.Value;
                   
                    SelectedItemCommand.NgaySinh = new DateTime(int.Parse(dateOfBirth.ToString("yyyy")), int.Parse(dateOfBirth.ToString("dd")), int.Parse(dateOfBirth.ToString("MM"))); 
                }
                ModifyDiagnosis diagnosis   = new ModifyDiagnosis(); 
                Messenger.Default.Send(SelectedItemCommand);
                diagnosis.ShowDialog();  
               
            }
            );
            ModifyAppointmentCommand = new RelayCommand<object>((p) => SelectedItemCommand != null, (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để sửa.");
                    return;
                }

                ModifyAppointment modifyWindow = new ModifyAppointment();
                Messenger.Default.Send(SelectedItemCommand); // Gửi bệnh nhân đã chọn
                modifyWindow.ShowDialog();

            });
        }


    }
}
