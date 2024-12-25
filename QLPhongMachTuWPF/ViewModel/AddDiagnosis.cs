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
using System.Windows.Media.Animation;

namespace QLPhongMachTuWPF.ViewModel
{
    public class AddDiagnosis : ViewModelBase
    {  
        public ICommand AddDiagnosisCommand { get; set; }
        public ICommand AddMedicineTolist { get; set; }

        #region ThuocTinh
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




        private ObservableCollection<NHANVIEN> _ListStaff { get; set; }

        public ObservableCollection<NHANVIEN> ListStaff { get => _ListStaff; set { _ListStaff = value; OnPropertyChanged(); } }

        private ObservableCollection<THUOC> _ListMedicine { get; set; }

        public ObservableCollection<THUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _ListChoice { get; set; }

        public ObservableCollection<string> ListChoice { get => _ListChoice; set { _ListChoice = value; OnPropertyChanged(); } }



        private string _MedicineChoice { get; set; }

        public string MedicineChoice { get => _MedicineChoice; set { _MedicineChoice = value; OnPropertyChanged(); } }
        private int _MaNV { get; set; }

        public int MaNV { get => _MaNV; set { _MaNV = value; OnPropertyChanged(); } }
        private int _MaBN { get; set; }
        public string Instruction { get => _Instruction; set { _Instruction = value; OnPropertyChanged(); } }
        private string _Instruction { get; set; }
        public int MaBN { get => _MaBN; set { _MaBN = value; OnPropertyChanged(); } }

        private string _SoLuong { get; set; }
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }
        #endregion

        #region FormatBindingDate

        public List<int> Days { get; set; }
        public List<int> Months { get; set; }
        public List<int> Years { get; set; }

        public List<int> DaysApp { get; set; }
        public List<int> MonthsApp { get; set; }
        public List<int> YearsApp { get; set; }

        public List<int> NumberSource { get; set; }


        public List<string> GenderSource { get; set; }
        public List<string> StatusSource { get; set; }

        private int _selectedDay;
        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                OnPropertyChanged();
            }
        }
        private int _selectedMonth;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
                UpdateDays();
                // Cập nhật số ngày khi tháng thay đổi
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
                UpdateDays();
                // Cập nhật số ngày khi năm thay đổi
            }
        }


        private int _selectedDayApp;
        public int SelectedDayApp
        {
            get => _selectedDayApp;
            set
            {
                _selectedDayApp = value;
                OnPropertyChanged();
            }
        }
        private int _selectedMonthApp;
        public int SelectedMonthApp
        {
            get => _selectedMonthApp;
            set
            {
                _selectedMonthApp = value;
                OnPropertyChanged();
                UpdateDaysApp();
                // Cập nhật số ngày khi tháng thay đổi
            }
        }

        private int _selectedYearApp;
        public int SelectedYearApp
        {
            get => _selectedYearApp;
            set
            {
                _selectedYearApp = value;
                OnPropertyChanged();
                UpdateDaysApp();

            }
        }


        public void AddSource()
        {
            SelectedYear = DateTime.Now.Year;
            SelectedMonth = DateTime.Now.Month;
            NumberSource = new List<int>();
            for (int i = 1; i <= 100; i++)
            {

                NumberSource.Add(i);
            }
            Years = new List<int>();
            for (int i = 1900; i <= 2100; i++)
                Years.Add(i);
            Months = new List<int>();
            for (int i = 1; i <= 12; i++)
                Months.Add(i);
            UpdateDays();



            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());

            GenderSource = new List<string> { "Nam", "Nữ" };
            StatusSource = new List<string> { "Available", "Unavailable" };

            // Khởi tạo số ngày theo tháng và năm mặc định  a


            SelectedYearApp = DateTime.Now.Year;
            SelectedMonthApp = DateTime.Now.Month;
            YearsApp = new List<int>();
            for (int i = 1900; i <= 2100; i++)
                YearsApp.Add(i);
            MonthsApp = new List<int>();
            for (int i = 1; i <= 12; i++)
                MonthsApp.Add(i);
            UpdateDaysApp();
        }
        private void UpdateDays()
        {
            try
            {
                if (SelectedYear < 1 || SelectedYear > 9999)
                    throw new ArgumentOutOfRangeException(nameof(SelectedYear), "Year must be between 1 and 9999.");

                int daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

                Days = new List<int>();
                for (int i = 1; i <= daysInMonth; i++)
                    Days.Add(i);

                if (SelectedDay > daysInMonth)
                    SelectedDay = daysInMonth;

                OnPropertyChanged(nameof(Days));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating days: {ex.Message}");
            }
        }
        private void UpdateDaysApp()
        {
            try
            {
                if (SelectedYearApp < 1 || SelectedYearApp > 9999)
                    throw new ArgumentOutOfRangeException(nameof(SelectedYearApp), "Year must be between 1 and 9999.");

                int daysInMonthApp = DateTime.DaysInMonth(SelectedYearApp, SelectedMonthApp);

                DaysApp = new List<int>();
                for (int i = 1; i <= daysInMonthApp; i++)
                    DaysApp.Add(i);

                if (SelectedDayApp > daysInMonthApp)
                    SelectedDayApp = daysInMonthApp;

                OnPropertyChanged(nameof(DaysApp));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating days: {ex.Message}");
            }
        }
        #endregion

        private string _SelectedItemCommand { get; set; }

        public string SelectedItemCommand
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
        private NHANVIEN _SelectedStaff { get; set; }
        public NHANVIEN SelectedStaff
        {
            get => _SelectedStaff;
            set
            {
                if (_SelectedStaff != value)
                {
                    _SelectedStaff = value;
                    OnPropertyChanged();
                }
            }
        }
        private PHIEUKHAM _Diagnosis { get; set; }

        public PHIEUKHAM Diagnosis { get => _Diagnosis; set { _Diagnosis = value; OnPropertyChanged(); } }

        private DateTime? _DateDiagnosis { get; set; }

        public DateTime? DateDiagnosis { get => _DateDiagnosis; set { _DateDiagnosis = value; OnPropertyChanged(); } }


        public AddDiagnosis()
        {
            AddSource();
            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Ins.db.THUOCs.ToList());
            ListChoice = new ObservableCollection<string>();
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
                    NgaySinh = new DateTime(int.Parse(Nam), int.Parse(Thang), int.Parse(Ngay)),
                    GioiTinh = Gender, 
                    TrangThai = 0,
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
                    NgayKham = new DateTime(int.Parse(NamKham), int.Parse(ThangKham), int.Parse(NgayKham)),
                    TrieuChung = Symtoms,
                    KetQua = Result,
                    TrangThai = (Status == "Available") ? 1 : 0,
                    BENHNHAN = newPatient,
                }; 
                try
                {
                    // Đảm bảo Messenger đã được đăng ký và đối tượng DiagnosisVM đã sẵn sàng
                    DataProvider.Ins.db.PHIEUKHAMs.Add(newDiagnosis);
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send(newDiagnosis);
                    MessageBox.Show("Thêm phiếu khám thành công");
                    //Messenger.Default.Send("NewAppointmentAdded");

                    ResetFields(); 
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
              
            });
            AddMedicineTolist = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                // Kiểm tra nếu thuốc đã tồn tại trong ListChoice
                if (ListChoice.Contains(MedicineChoice))
                {
                    MessageBox.Show("Thuốc này đã được thêm rồi");
                    Instruction = string.Empty;
                    MedicineChoice = null;
                    SoLuong = null;
                    return;
                }

                // Kiểm tra xem thuốc có tồn tại trong cơ sở dữ liệu không
                var existingItem = DataProvider.Ins.db.THUOCs.FirstOrDefault(x => x.TenThuoc == MedicineChoice);
                if (existingItem == null)
                {
                    MessageBox.Show("Thuốc không tồn tại trong danh sách thuốc.");
                    Instruction = string.Empty;
                    MedicineChoice = null;
                    SoLuong = null;
                    return;
                }

                // Thêm thuốc mới vào cơ sở dữ liệu CTTT
                var MedicineDetails = new CTTT()
                {
                    MaThuoc = existingItem.MaThuoc,
                    MaPK = Diagnosis.MaPK,
                    SoLuong = int.Parse(SoLuong),
                    DonGia = existingItem.Gia,
                    CachDung = Instruction,
                    TrangThai = existingItem.TrangThai,
                };

                DataProvider.Ins.db.CTTTs.Add(MedicineDetails);
                DataProvider.Ins.db.SaveChanges();

                // Cập nhật ListChoice và xóa các trường nhập liệu
                ListChoice.Add(MedicineChoice);
                Instruction = string.Empty;
                MedicineChoice = null;
                SoLuong = null;

                MessageBox.Show("Thêm thuốc thành công!");
            });
        } private void ResetFields()
            {
                TenBN = string.Empty;
                DiaChi = string.Empty;
                DienThoai = string.Empty;
               
             
                TenNV = string.Empty;
               
                Symtoms = string.Empty;
                Result = string.Empty;
            }
    }

}