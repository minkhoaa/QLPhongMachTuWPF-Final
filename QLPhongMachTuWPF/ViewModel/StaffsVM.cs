using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{

    public class StaffsVM : ViewModelBase
    {
        public ICommand AddStaffCommand { get; set; }

        public ICommand ModifyStaffsCommand { get; set; }

        public ICommand DeleteStaffCommand { get; set; }

        private NHANVIEN _SelectedItemCommand { get; set; }

        public NHANVIEN SelectedItemCommand
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

        public ICollectionView FilteredStaffs { get; set; }


        private ObservableCollection<NHANVIEN> _staff;
        public ObservableCollection<NHANVIEN> StaffList { get => _staff; set { _staff = value; OnPropertyChanged(); } }
        public StaffsVM()
        {
            StaffList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs);

            FilteredStaffs = CollectionViewSource.GetDefaultView(StaffList);

            Messenger.Default.Register<NHANVIEN>(this, (staff) =>
            {
                if (staff == null) return;

                // Tìm đối tượng trong danh sách hiện tại
                var existingStaff = StaffList.FirstOrDefault(p => p.MaNV == staff.MaNV ||
                (p.TenNV == staff.TenNV && p.DienThoai == staff.DienThoai && p.DiaChi == staff.DiaChi && p.NgaySinh == staff.NgaySinh));
                if (existingStaff != null)
                {
                    // Cập nhật thông tin
                    existingStaff.TenNV = staff.TenNV;
                    existingStaff.DiaChi = staff.DiaChi;
                    existingStaff.GioiTinh = staff.GioiTinh;
                    existingStaff.DienThoai = staff.DienThoai;
                    existingStaff.NgaySinh = staff.NgaySinh;
                    existingStaff.TrangThai = staff.TrangThai;
                    existingStaff.LoaiNV = staff.LoaiNV;
                }
                else
                {
                    // Thêm nhân viên mới
                    StaffList.Add(staff);
                }

                // Ghi thay đổi vào database
                DataProvider.Ins.db.SaveChanges();
                FilteredStaffs?.Refresh();
            });

            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff();
                add.ShowDialog();
            }
            );

            ModifyStaffsCommand = new RelayCommand<object>((p) => SelectedItemCommand != null, (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
                    return;
                }

                ModifyStaff modifyWindow = new ModifyStaff();
                Messenger.Default.Send(SelectedItemCommand); // Gửi bệnh nhân đã chọn
                modifyWindow.ShowDialog();

            });
            DeleteStaffCommand = new RelayCommand<object>((p) => SelectedItemCommand != null, (p) =>
            {
                if (SelectedItemCommand == null)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
                    return;
                }
                foreach (var item in DataProvider.Ins.db.HOADONs)
                {
                    if (item.PHIEUKHAM.MaNV == SelectedItemCommand.MaNV)
                    {
                        DataProvider.Ins.db.HOADONs.Remove(item);
                    }

                }
                foreach (var diagnosis in DataProvider.Ins.db.PHIEUKHAMs)
                {
                    if (diagnosis.MaNV == SelectedItemCommand.MaNV)
                    {
                        
                        var appointment = DataProvider.Ins.db.LICHHENs.FirstOrDefault(x => x.MaPK == diagnosis.MaPK);
                        if (appointment != null)
                        {

                            DataProvider.Ins.db.LICHHENs.Remove(appointment);
                        }
                        foreach (var item in DataProvider.Ins.db.CTTTs)
                        {
                            if (item.MaPK == diagnosis.MaPK)
                            {
                                DataProvider.Ins.db.CTTTs.Remove(item);
                            }

                        }
                      

                        DataProvider.Ins.db.PHIEUKHAMs.Remove(diagnosis);
                    }
                }


                DataProvider.Ins.db.SaveChanges();
                DataProvider.Ins.db.NHANVIENs.Remove(SelectedItemCommand);

                try
                {
                    DataProvider.Ins.db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    // Cập nhật danh sách `PHIEUKHAM`
                    StaffList.Remove(SelectedItemCommand);

                    // Làm mới danh sách `LICHHEN`
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");

                    MessageBox.Show("Xóa thành công");
                    FilteredStaffs.Refresh(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message.ToString());
                }
                FilteredStaffs.Refresh();

            });

        }
 
        void FilterStaffs()
        {
            if (FilteredStaffs == null)
                return;

            // Gán bộ lọc
            FilteredStaffs.Filter = (obj) =>
            {
                if (obj is NHANVIEN staffs)
                {
                    // Kiểm tra từ khóa tìm kiếm, không phân biệt chữ hoa/chữ thường
                    return string.IsNullOrEmpty(SearchKeyword) ||
                           staffs.TenNV?.IndexOf(SearchKeyword, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };

            // Làm mới view để cập nhật kết quả lọc
            FilteredStaffs.Refresh();
        }
    }
}
