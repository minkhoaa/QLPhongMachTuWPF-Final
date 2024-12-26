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

        private ICollectionView _FilteredInvoice {  get; set; }
        public ICollectionView FilteredInvoice { get => _FilteredInvoice; set { _FilteredInvoice = value; OnPropertyChanged(); } }

        private HOADON _SelectedInvoice { get; set; }
        public HOADON SelectedInvoice { get => _SelectedInvoice; set { _SelectedInvoice = value; OnPropertyChanged(); } }

        public InvoiceVM()
            {
                InvoiceList = new ObservableCollection<HOADON>(DataProvider.Ins.db.HOADONs );
                FilteredInvoice = CollectionViewSource.GetDefaultView(InvoiceList);
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
                        existingInvoice.NgayHD = invoice .NgayHD;
                        existingInvoice.TongTien = invoice .TongTien;  
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

                });
            });
            ModifyInvoiceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailInovice diagnosis = new DetailInovice();
                Messenger.Default.Send(SelectedInvoice);
                diagnosis.ShowDialog();
            }
           );
            VerifyCommand = new RelayCommand<object>((p) => SelectedInvoice != null, (p) =>
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
            });


            Messenger.Default.Register<string>(this, (message) =>
            {
                if (message == "RefreshInvoiceList")
                {
                   // RefreshInvoiceList();
                }
            });



        }
        private decimal CalculateTienThuoc(int MaPK)
        {
            // Lấy danh sách chi tiết thuốc cho hóa đơn
            var chiTietThuocs = DataProvider.Ins.db.CTTTs.Where(x => x.MaPK == MaPK).ToList();

            // Tính tổng tiền thuốc = ∑(Số lượng * Giá tiền)
            return (decimal)chiTietThuocs.Sum(x => x.SoLuong * x.DonGia);
        }

        private void RefreshInvoiceList()
        {
            // Lấy danh sách hóa đơn từ cơ sở dữ liệu
            var updatedInvoiceList = DataProvider.Ins.db.HOADONs.ToList();

            // Cập nhật TienThuoc cho từng hóa đơn từ bảng CTTT
            foreach (var invoice in updatedInvoiceList)
            {
                invoice.TienThuoc = CalculateTienThuoc(invoice.MaHD);
            }

            // Cập nhật danh sách InvoiceList
            InvoiceList.Clear();
            foreach (var invoice in updatedInvoiceList)
            {
                InvoiceList.Add(invoice);
            }

            // Làm mới CollectionView nếu cần
            FilteredInvoice.Refresh();
        }



    }

}

