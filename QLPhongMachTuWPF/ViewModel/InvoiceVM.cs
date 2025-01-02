using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{

   
        public class InvoiceVM : ViewModelBase
        {
        public ICommand AddInvoiceCommand { get; set; }

        public ICommand ModifyInvoiceCommand { get; set; }
        public ICommand VerifyCommand { get; set; }

        public ICommand DeleteInvoiceCommand { get; set; }


        private ObservableCollection<HOADON> _invoice;
        public ObservableCollection<HOADON> InvoiceList { get => _invoice; set { _invoice = value; OnPropertyChanged(); } }

        public ICollectionView FilteredInvoice {  get; set; }
     
        private HOADON _SelectedInvoice { get; set; }
        public HOADON SelectedInvoice { get => _SelectedInvoice; set { _SelectedInvoice = value; OnPropertyChanged(); } }

        #region Filter

        private DateTime? _FilterDateFrom = new DateTime(2000,1,1);
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

        private DateTime? _FilterDateTo = new DateTime(2030, 1, 1);
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
        public void FilterDate()
        {


            if (FilterDateFrom != null && FilterDateTo != null)
            {
                FilteredInvoice.Filter = (item) =>
                {
                    var diagnosis = item as HOADON;
                    if (diagnosis == null || diagnosis.NgayHD == null) return false;

                    // So sánh ngày khám với khoảng thời gian được lọc
                    return diagnosis.NgayHD >= FilterDateFrom && diagnosis.NgayHD <= FilterDateTo;
                };
                FilteredInvoice.Refresh();
            }
            else
            {
                // Nếu không có ngày lọc, loại bỏ bộ lọc

                FilteredInvoice.Refresh();
            }
        }
        private string _SearchKeyword { get; set; }

        public string SearchKeyword
        {
            get => _SearchKeyword;
            set
            {
                _SearchKeyword = value;
                OnPropertyChanged();
                FilterStaffs();
            }
        }
        void FilterStaffs()
        {
            if (FilteredInvoice == null)
                return;


            // Gán bộ lọc
            FilteredInvoice.Filter = (obj) =>
            {
                if (obj is HOADON staffs)
                {
                    // Kiểm tra từ khóa tìm kiếm, không phân biệt chữ hoa/chữ thường
                    return string.IsNullOrEmpty(SearchKeyword) ||
                           staffs.PHIEUKHAM.BENHNHAN.TenBN?.IndexOf(SearchKeyword, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };

            // Làm mới view để cập nhật kết quả lọc
            FilteredInvoice.Refresh();
        }
        #endregion
        public InvoiceVM()
            {
            Messenger.Default.Send("Refresh", "RefreshInvoiceList");
            Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
            Messenger.Default.Send("Refresh", "RefreshAppointmentList");
            InvoiceList = new ObservableCollection<HOADON>(DataProvider.Ins.db.HOADONs );
                FilteredInvoice = CollectionViewSource.GetDefaultView(InvoiceList);

            try
            {
                AddInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    AddInvoice add = new AddInvoice();
                    add.ShowDialog();
                }
                );
                Messenger.Default.Register<HOADON>(this, (invoice) =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                        {
                   if (invoice == null) return;

                   var existingInvoice = DataProvider.Ins.db.HOADONs.FirstOrDefault(x => x.MaHD == invoice.MaHD);
                   if (existingInvoice != null)
                   {
                       existingInvoice.MaPK = invoice.MaPK;
                       existingInvoice.NgayHD = invoice.NgayHD;
                       existingInvoice.TongTien = invoice.TongTien;
                       existingInvoice.TienThuoc = invoice.TienThuoc;
                       existingInvoice.TienKham = invoice.TienKham;
                       existingInvoice.TrangThai = invoice.TrangThai;
                   }
                   else
                   {
                       InvoiceList.Add(invoice);

                   }
                   DataProvider.Ins.db.SaveChanges();
                   FilteredInvoice.Refresh();
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");

                        });
                   });
                ModifyInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");
                    ModifyDetailInvoice diagnosis = new ModifyDetailInvoice();
                    Messenger.Default.Send(SelectedInvoice);
                    diagnosis.ShowDialog();
                }
               );
                VerifyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    ModifyDetailInvoice diagnosis = new ModifyDetailInvoice();
                    Messenger.Default.Send(SelectedInvoice); // Gửi hóa đơn hiện tại đến form sửa đổi
                    diagnosis.ShowDialog(); // Hiển thị form sửa đổi

                    // Làm mới dữ liệu sau khi sửa đổi

                });
                DeleteInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    if (SelectedInvoice == null)
                    {
                        MessageBox.Show("Vui lòng chọn một hóa đơn để xóa.");
                        return;
                    }



                    DataProvider.Ins.db.HOADONs.Remove(SelectedInvoice);
                    try
                    {
                        if (InvoiceList.Remove(SelectedInvoice))
                        {
                            MessageBox.Show("Xóa thành công");
                          

                        }

                        
                    }

                    catch (Exception ex) { MessageBox.Show("Xóa thất bại" + ex.Message.ToString()); }
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send("Refresh", "RefreshMedicineList");
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");
                });

                Messenger.Default.Register<string>(this, "RefreshInvoiceList", (message) =>
                {
                    if (message == "Refresh")
                    {
                        RefreshInvoiceList();
                    }

                });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void RefreshInvoiceList() {
            Application.Current.Dispatcher.Invoke(() =>
            {
                InvoiceList.Clear(); // Xóa danh sách cũ
                var appointments = DataProvider.Ins.db.HOADONs.ToList(); // Lấy danh sách mới từ DB
                foreach (var appointment in appointments)
                {
                    InvoiceList.Add(appointment);

                }
            });
        }



    }

}

