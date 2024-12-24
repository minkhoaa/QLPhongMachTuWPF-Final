using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shell;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyDiagnosisVM  : ViewModelBase
    {
        public ICommand ConfirmCommand { get; set; }

        public ICommand CreateInvoice { get; set; }

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
        private int  _MaBN { get; set; }
        public string Instruction { get => _Instruction; set { _Instruction = value; OnPropertyChanged(); } }
        private string _Instruction { get; set; }
        public int  MaBN { get => _MaBN; set { _MaBN = value; OnPropertyChanged(); } }

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
            
            GenderSource = new List<string> {"Nam", "Nữ" };
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
        private PHIEUKHAM _Diagnosis {  get; set; }
       public PHIEUKHAM Diagnosis { get => _Diagnosis; set { _Diagnosis = value; OnPropertyChanged();  } }
        public ModifyDiagnosisVM()
        {

            AddSource();
            ListStaff = new ObservableCollection<NHANVIEN>(DataProvider.Ins.db.NHANVIENs.ToList());
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Ins.db.THUOCs.ToList());
            ListChoice = new ObservableCollection<string>();  
            
            Messenger.Default.Register<LICHHEN>(this, (diagnosis) =>
            { 
                
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    TenNV =  diagnosis.TenNV;
                    TenBN = diagnosis.TenBN;
                    DateTime dateOfBirth = diagnosis.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = dateOfBirth.Month.ToString();
                    Nam = dateOfBirth.Year.ToString();
                    DateTime dateTreat = diagnosis.NgayKham.Value;
                    NgayKham = dateTreat.Day.ToString();
                    ThangKham = dateTreat.Month.ToString();
                    NamKham = dateTreat.Year.ToString();

                    DiaChi = diagnosis.DiaChi;
                    DienThoai = diagnosis.DienThoai;
                    Gender = diagnosis.GioiTinh ;
                    Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";  
                });
                

            });
            Messenger.Default.Register<PHIEUKHAM>(this, (diagnosis) =>
            {
                ListChoice = new ObservableCollection<string>();
                foreach (var i in DataProvider.Ins.db.CTTTs)
                {
                    if (i.MaPK == diagnosis.MaPK) ListChoice.Add(i.THUOC.TenThuoc.ToString());
                    
                }
                Diagnosis = (PHIEUKHAM)diagnosis;
                var Staff = DataProvider.Ins.db.NHANVIENs.FirstOrDefault(a => a.MaNV == diagnosis.MaNV);
                var Patient = DataProvider.Ins.db.BENHNHANs.FirstOrDefault(a => a.MaBN == diagnosis.MaBN);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MaNV = (int)diagnosis.MaNV;
                    MaBN = (int)diagnosis.MaBN; 
                    TenNV = Staff.TenNV;
                    TenBN = Patient.TenBN;
                    DateTime dateOfBirth = Patient.NgaySinh.Value;
                    Ngay = dateOfBirth.Day.ToString();
                    Thang = dateOfBirth.Month.ToString();
                    Nam = dateOfBirth.Year.ToString();
                    DateTime dateTreat = diagnosis.NgayKham.Value;
                    NgayKham = dateTreat.Day.ToString();
                    ThangKham = dateTreat.Month.ToString();
                    NamKham = dateTreat.Year.ToString();

                    DiaChi = Patient.DiaChi;
                    DienThoai = Patient.DienThoai;
                    Gender = Patient.GioiTinh;
                    Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";
                    Symtoms = diagnosis.TrieuChung;
                    Result = diagnosis.KetQua; 

                });
            });
            AddMedicineTolist = new RelayCommand<object>((p) =>
            {
                return true;
               }, (p) => {
                THUOC Medicine = DataProvider.Ins.db.THUOCs.FirstOrDefault(x => x.TenThuoc == MedicineChoice) as THUOC;
                   var cttt = DataProvider.Ins.db.CTTTs.FirstOrDefault(x => (x.MaThuoc == Medicine.MaThuoc) && (Diagnosis.MaPK == x.MaPK));
                   try { 
                       if (Medicine != null)
                       {
                           var MedicineDetails = new CTTT()
                           {
                               MaThuoc = Medicine.MaThuoc,
                               MaPK = Diagnosis.MaPK,
                               SoLuong = int.Parse(SoLuong),
                               DonGia = Medicine.Gia,
                               CachDung = Instruction.ToString(),
                               TrangThai = Medicine.TrangThai,
                           };
                           DataProvider.Ins.db.CTTTs.Add(MedicineDetails);
                           DataProvider.Ins.db.SaveChanges();

                           ListChoice.Add(MedicineChoice);
                       }
                   }
                   catch {
                       MessageBox.Show("Phiếu khám này đã được kê đơn");
                   }
                

            });
            CreateInvoice = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => {

                Diagnosis.MaNV = MaNV;
                Diagnosis.MaBN = MaBN;
                Diagnosis.TrieuChung = Symtoms;
                Diagnosis.KetQua = Result;
                Diagnosis.TrangThai = (Status == "Available") ? 1 : 0;
                Diagnosis.NgayKham = new DateTime(int.Parse(NamKham), int.Parse(ThangKham), int.Parse(NgayKham));




                    
                
                DetailInovice detailInovice = new DetailInovice();
               Messenger.Default.Send(Diagnosis);
                detailInovice.ShowDialog();

            });

        }
    }
}
