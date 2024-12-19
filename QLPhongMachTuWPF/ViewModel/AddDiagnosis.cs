﻿using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Media.Animation;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddDiagnosis : ViewModelBase
    {  
        public ICommand AddDiagnosisCommand { get; set; }
        private string _TenBN { get; set; }

        public string TenBN { get => _TenBN; set { _TenBN = value; OnPropertyChanged(); } }

        private string _DiaChi { get; set; }

        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }


        private string _DienThoai { get; set; }

        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }


        private string _Ngay { get; set; }

        public string Ngay { get => _Ngay; set { _Ngay = value; OnPropertyChanged(); } }

        private string _Thang { get; set; }

        public string Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }

        private string _Nam { get; set; }

        public string Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }

        private string _Gender { get; set; }

        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Status { get; set; }

        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private string _NgayKham { get; set; }

        public string NgayKham { get => _NgayKham; set { _NgayKham = value; OnPropertyChanged(); } }

        private string _ThangKham { get; set; }

        public string ThangKham { get => _ThangKham; set { _ThangKham = value; OnPropertyChanged(); } }

        private string _NamKham { get; set; }

        public string NamKham { get => _NamKham; set { _NamKham = value; OnPropertyChanged(); } }

        private string _TenNV { get; set; }

        public string TenNV { get => _TenNV; set { _TenNV = value; OnPropertyChanged(); } }

        private string _Symtoms { get; set; }

        public string Symtoms { get => _Symtoms; set { _Symtoms = value; OnPropertyChanged(); } }

        private string _Result { get; set; }

        public string Result { get => _Result; set { _Result = value; OnPropertyChanged(); } }
        public int CheckMonth(string Thang)
        {
            switch (Thang)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    return -1;
            }
        }

        public AddDiagnosis()
        {
            AddDiagnosisCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenBN)) return false;
                return true;
            }, (p) => {
                // Khởi tạo đối tượng mới
                var newPatient = new BENHNHAN()
                {
                    TenBN = TenBN,
                    DiaChi = DiaChi,
                    DienThoai = DienThoai,
                    NgaySinh = new DateTime(int.Parse(Nam), CheckMonth(Thang), int.Parse(Ngay)),
                    GioiTinh = (Gender == "Male") ? "Nam" : "Nữ", 
                    TrangThai = 0
                };

                try
                {
                    var existingPatients = DataProvider.Ins.db.BENHNHANs.FirstOrDefault(a => a.TenBN == newPatient.TenBN && a.NgaySinh == newPatient.NgaySinh);
                    if (existingPatients == null)
                    {
                        DataProvider.Ins.db.BENHNHANs.Add(newPatient);
                    }
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newPatient);
                }
                catch
                (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                var nhanVien = DataProvider.Ins.db.NHANVIENs.FirstOrDefault(nv => nv.TenNV == TenNV);
                if (nhanVien == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên với tên này.");
                    return;
                }

                var benhNhan = DataProvider.Ins.db.BENHNHANs.FirstOrDefault(bn => bn.TenBN == TenBN);
                if (benhNhan == null)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân với tên này.");
                    return;
                }

                // Tạo đối tượng PHIEUKHAM và gửi
                PHIEUKHAM newDiagnosis = new PHIEUKHAM()
                {
                    MaNV = nhanVien.MaNV,
                    MaBN = benhNhan.MaBN,
                    NgayKham = new DateTime(int.Parse(NamKham), CheckMonth(ThangKham), int.Parse(NgayKham)),
                    TrieuChung = Symtoms,
                    KetQua = Result,
                    TrangThai = (Status == "Available") ? 1 : 0,
                };
                try
                {
                    // Đảm bảo Messenger đã được đăng ký và đối tượng DiagnosisVM đã sẵn sàng
                    DataProvider.Ins.db.PHIEUKHAMs.Add(newDiagnosis);
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newDiagnosis);
                    MessageBox.Show("Thêm phiếu khám thành công");
                    ResetFields(); 
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
              
            });
        } private void ResetFields()
            {
                TenBN = string.Empty;
                DiaChi = string.Empty;
                DienThoai = string.Empty;
                Ngay = string.Empty;
                Thang = string.Empty;
                Nam = string.Empty;
                Gender = string.Empty;
                Status = string.Empty;
                TenNV = string.Empty;
                NgayKham = string.Empty;
                ThangKham = string.Empty;
                NamKham = string.Empty;
                Symtoms = string.Empty;
                Result = string.Empty;
            }
    }

}