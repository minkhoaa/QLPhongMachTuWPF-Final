using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup.Localizer;

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

        public ICommand DeleteAppointmentCommand { get; set; }

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
                        existingAppointment.MaNV = Appointment.MaNV;
                        existingAppointment.MaBN = Appointment.MaBN;
                        existingAppointment.NgayHen = Appointment.NgayHen;
                        existingAppointment.GioHen = Appointment.GioHen;
                        existingAppointment.MaPK = Appointment.MaPK;
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
                var exisitedDiagnosis = DataProvider.Ins.db.PHIEUKHAMs.Where(x => x.MaPK == SelectedItemCommand.MaPK && x.MaBN == SelectedItemCommand.MaBN).FirstOrDefault() as PHIEUKHAM;
                ModifyDiagnosis diagnosis = new ModifyDiagnosis();
                Messenger.Default.Send(exisitedDiagnosis);
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
            DeleteAppointmentCommand = new RelayCommand<object>((p) => { return true; } , (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để xóa.");
                    return;
                }

               DataProvider.Ins.db.LICHHENs.Remove( SelectedItemCommand );
                try { 
                if  (AppointmentList.Remove(SelectedItemCommand))
                {
                        MessageBox.Show("Xóa thành công");

                    } }
                catch (Exception ex) { MessageBox.Show("Xóa thất bại" + ex.Message.ToString());  }
                DataProvider.Ins.db.SaveChanges();

            });
            Messenger.Default.Register<string>(this, "RefreshAppointmentList", (message) =>
            {
                if (message == "Refresh")
                {
                    RefreshAppointmentList();
                }
            });


        }

        public void RefreshAppointmentList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AppointmentList.Clear(); // Xóa danh sách cũ
                var appointments = DataProvider.Ins.db.LICHHENs.ToList(); // Lấy danh sách mới từ DB
                foreach (var appointment in appointments)
                {
                    AppointmentList.Add(appointment);

                }
            });
        }

    }
}
