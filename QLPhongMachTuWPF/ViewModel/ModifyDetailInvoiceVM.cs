using GalaSoft.MvvmLight.Messaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ModifyDetailInvoiceVM : ViewModelBase
    {
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

        private string _ID { get; set; }

        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        private DateTime _NgayHoanThanh { get; set; }

        public DateTime NgayHoanThanh { get => _NgayHoanThanh; set { _NgayHoanThanh = value; OnPropertyChanged(); } }

        public ObservableCollection<NHANVIEN> ListStaff { get => _ListStaff; set { _ListStaff = value; OnPropertyChanged(); } }

        private ObservableCollection<CTTT> _ListMedicine { get; set; }

        public ObservableCollection<CTTT> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        public ICollectionView CollectionViewMedicine { get; set; }



        #endregion
        #region FormatBindingDate

        public List<int> Days { get; set; }
        public List<int> Months { get; set; }
        public List<int> Years { get; set; }

        public List<int> DaysApp { get; set; }
        public List<int> MonthsApp { get; set; }
        public List<int> YearsApp { get; set; }



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


        private CTTT _SelectedMedicine { get; set; }
        public CTTT SelectedMedicine { get => _SelectedMedicine; set { _SelectedMedicine = value; OnPropertyChanged(); } }

        public HOADON tempInvoice { get; set; } 
        public ICommand SaveChangesCommand { get; set; }

        public ICommand PrintCommand { get; set; }

       

        public ModifyDetailInvoiceVM()
        {
            // Khởi tạo danh sách thuốc ban đầu
            ListMedicine = new ObservableCollection<CTTT>();
            CollectionViewMedicine = CollectionViewSource.GetDefaultView(ListMedicine);

            AddSource();
            try
            {
                // Lắng nghe thông điệp từ Messenger
                Messenger.Default.Register<PHIEUKHAM>(this, (diagnosis) =>
                {

                    if (diagnosis == null) return;
                    ListMedicine.Clear(); // Đảm bảo danh sách không bị dữ liệu cũ
                    var medicines = DataProvider.Ins.db.CTTTs.Where(x => x.MaPK == diagnosis.MaPK).ToList();
                    foreach (var medicine in medicines)
                    {
                        ListMedicine.Add(medicine);
                    }

                    CollectionViewMedicine.Refresh();


                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        ID = diagnosis.MaPK.ToString();
                        TenNV = diagnosis.NHANVIEN.TenNV;
                        TenBN = diagnosis.BENHNHAN.TenBN;
                        Ngay = diagnosis.BENHNHAN.NgaySinh?.Day.ToString() ?? "N/A";
                        Thang = diagnosis.BENHNHAN.NgaySinh?.Month.ToString() ?? "N/A";
                        Nam = diagnosis.BENHNHAN.NgaySinh?.Year.ToString() ?? "N/A";
                        NgayKham = diagnosis.NgayKham?.Day.ToString() ?? "N/A";
                        ThangKham = diagnosis.NgayKham?.Month.ToString() ?? "N/A";
                        NamKham = diagnosis.NgayKham?.Year.ToString() ?? "N/A";
                        DiaChi = diagnosis.BENHNHAN.DiaChi;
                        DienThoai = diagnosis.BENHNHAN.DienThoai;
                        Gender = diagnosis.BENHNHAN.GioiTinh;
                        Status = (diagnosis.TrangThai == 1) ? "Available" : "Unavailable";
                        Symtoms = diagnosis.TrieuChung;
                        Result = diagnosis.KetQua;
                        NgayHoanThanh = DateTime.Now;
                    });
                });
                Messenger.Default.Register<LICHHEN>(this, (appointment) =>
                {

                    if (appointment == null) return;
                    ListMedicine.Clear(); // Đảm bảo danh sách không bị dữ liệu cũ
                    var medicines = DataProvider.Ins.db.CTTTs.Where(x => x.MaPK == appointment.MaPK).ToList();
                    foreach (var medicine in medicines)
                    {
                        ListMedicine.Add(medicine);
                    }

                    CollectionViewMedicine.Refresh();


                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ID = appointment.MaPK.ToString();
                        TenNV = appointment.NHANVIEN.TenNV;
                        TenBN = appointment.BENHNHAN.TenBN;
                        Ngay = appointment.BENHNHAN.NgaySinh?.Day.ToString() ?? "N/A";
                        Thang = appointment.BENHNHAN.NgaySinh?.Month.ToString() ?? "N/A";
                        Nam = appointment.BENHNHAN.NgaySinh?.Year.ToString() ?? "N/A";
                        NgayKham = appointment.NgayHen.Value.Day.ToString() ?? "N/A";
                        ThangKham = appointment.NgayHen.Value.Day.ToString() ?? "N/A";
                        NamKham = appointment.NgayHen.Value.Day.ToString() ?? "N/A";
                        DiaChi = appointment.BENHNHAN.DiaChi;
                        DienThoai = appointment.BENHNHAN.DienThoai;
                        Gender = appointment.BENHNHAN.GioiTinh;
                        Status = "Unpaid";
                        Symtoms = appointment.PHIEUKHAM.TrieuChung;
                        Result = appointment.PHIEUKHAM.KetQua;
                        NgayHoanThanh = DateTime.Now;
                    });
                });


                Messenger.Default.Register<HOADON>(this, (invoice) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        tempInvoice = invoice;

                        if (invoice == null) return;
                        ListMedicine.Clear(); // Đảm bảo danh sách không bị dữ liệu cũ
                        var medicines = DataProvider.Ins.db.CTTTs.Where(x => x.MaPK == invoice.MaPK).ToList();
                        foreach (var medicine in medicines)
                        {
                            ListMedicine.Add(medicine);
                        }

                        CollectionViewMedicine.Refresh();


                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                ID = invoice.MaHD.ToString();
                                TenNV = invoice.PHIEUKHAM.NHANVIEN.TenNV;
                                TenBN = invoice.PHIEUKHAM.BENHNHAN.TenBN;
                                Ngay = invoice.PHIEUKHAM.BENHNHAN.NgaySinh.Value.Day.ToString() ?? "N/A";
                                Thang = invoice.PHIEUKHAM.BENHNHAN.NgaySinh.Value.Month.ToString() ?? "N/A";
                                Nam = invoice.PHIEUKHAM.BENHNHAN.NgaySinh.Value.Year.ToString() ?? "N/A";
                                NgayKham = invoice.NgayHD.Value.Day.ToString() ?? "N/A";
                                ThangKham = invoice.NgayHD.Value.Day.ToString() ?? "N/A";
                                NamKham = invoice.NgayHD.Value.Day.ToString() ?? "N/A";
                                DiaChi = invoice.PHIEUKHAM.BENHNHAN.DiaChi;
                                DienThoai = invoice.PHIEUKHAM.BENHNHAN.DienThoai;
                                Gender = invoice.PHIEUKHAM.BENHNHAN.GioiTinh;
                                Status = "Unpaid";
                                Symtoms = invoice.PHIEUKHAM.TrieuChung;
                                Result = invoice.PHIEUKHAM.KetQua;
                                NgayHoanThanh = DateTime.Now;
                            } catch (Exception ex) { MessageBox.Show(ex.Message);  } 
                        });
                    });
                });

                SaveChangesCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {

                    tempInvoice.PHIEUKHAM.BENHNHAN.DiaChi = DiaChi;
                    tempInvoice.PHIEUKHAM.BENHNHAN.TenBN = TenBN;
                    tempInvoice.PHIEUKHAM.BENHNHAN.DienThoai = DienThoai;
                    tempInvoice.PHIEUKHAM.BENHNHAN.GioiTinh = Gender;

                    foreach (var medicine in ListMedicine)
                    {
                        var dbMedicine = DataProvider.Ins.db.CTTTs.FirstOrDefault(x => x.MaPK == tempInvoice.MaPK && x.MaThuoc == medicine.MaThuoc);
                        if (dbMedicine != null)
                        {
                            dbMedicine.CachDung = medicine.CachDung;
                            dbMedicine.SoLuong = medicine.SoLuong; // Số lượng
                            dbMedicine.DonGia = medicine.THUOC.Gia * medicine.SoLuong;   // Đơn giá

                        }
                    }
                    DataProvider.Ins.db.SaveChanges();
                    Messenger.Default.Send("RefreshInvoiceList");

                    Application.Current.Windows
                  .OfType<Window>()
                  .SingleOrDefault(w => w.IsActive)
                  ?.Close();

                });
                PrintCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    tempInvoice.TrangThai = 1;
                    tempInvoice.PHIEUKHAM.TrangThai = 1;
                    tempInvoice.PHIEUKHAM.BENHNHAN.TrangThai = 1; 

                    var appoinment = DataProvider.Ins.db.LICHHENs.FirstOrDefault(x => x.PHIEUKHAM.MaPK == tempInvoice.PHIEUKHAM.MaPK);
                    if (appoinment != null)
                    {
                        DataProvider.Ins.db.LICHHENs.Remove(appoinment);
                        DataProvider.Ins.db.SaveChanges();
                    }

                    Messenger.Default.Send(tempInvoice);
                    DataProvider.Ins.db.SaveChanges();


                    Messenger.Default.Send("RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshInvoiceList");
                    Messenger.Default.Send("Refresh", "RefreshDiagnosisList");
                    Messenger.Default.Send("Refresh", "RefreshAppointmentList");
                    PrintInvoice();


                }
             );

                DataProvider.Ins.db.SaveChanges();
                Messenger.Default.Send("RefreshInvoiceList");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        #region CreateInvoiceToPrint
        public string FormatCurrency(decimal amount)
        {
            return string.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:#,##0}", amount) + " VND";
        }

        public void PrintInvoice()
        {
            var receiptContent = GenerateReceiptFromView(tempInvoice);

            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Invoice_POS",
                DefaultExt = ".pdf",
                Filter = "PDF files (.pdf)|*.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SavePdfFile(receiptContent, saveFileDialog.FileName);
                MessageBox.Show("Hóa đơn đã được lưu!");
            }
        }



        public void SavePdfFile(string content, string filePath)
        {
            try
            {
                // Khởi tạo tài liệu PDF
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                // Mở tài liệu để ghi
                document.Open();

                // Thêm tiêu đề
                var titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
                Paragraph title = new Paragraph("INVOICE", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                // Thêm thông tin khách hàng và ngày
                var infoFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                document.Add(new Paragraph($"Invoice Code: #{tempInvoice.MaHD}", infoFont));
                document.Add(new Paragraph($"Invoice Date: {tempInvoice.NgayHD:dd/MM/yyyy}", infoFont));
                document.Add(new Paragraph("--------------------------------", infoFont));
                document.Add(new Paragraph($"Customer Name: {RemoveDiacritics(tempInvoice.PHIEUKHAM.BENHNHAN.TenBN)}", infoFont));
                document.Add(new Paragraph($"Birthday: {tempInvoice.PHIEUKHAM.BENHNHAN.NgaySinh:dd/MM/yyyy}", infoFont));
                document.Add(new Paragraph($"Phone Number: {tempInvoice.PHIEUKHAM.BENHNHAN.DienThoai}", infoFont));
                document.Add(new Paragraph($"Address: {RemoveDiacritics(tempInvoice.PHIEUKHAM.BENHNHAN.DiaChi)}", infoFont));
                document.Add(new Paragraph($"Gender: {RemoveDiacritics(tempInvoice.PHIEUKHAM.BENHNHAN.GioiTinh)}", infoFont));



                document.Add(new Paragraph("--------------------------------", infoFont));
                document.Add(new Paragraph("      ", infoFont));
                // Thêm bảng các mặt hàng
                PdfPTable table = new PdfPTable(4);  // 4 cột: Tên SP, SL, Đơn Giá, Thành Tiền
                table.WidthPercentage = 100;  // Độ rộng của bảng

                // Đặt tên các cột
                table.AddCell("Medicine Name");
                table.AddCell("Quantity");
                table.AddCell("UnitPrice");
                table.AddCell("Price");

                foreach (var item in tempInvoice.PHIEUKHAM.CTTTs)
                {
                    table.AddCell(RemoveDiacritics((item.THUOC.TenThuoc)));
                    table.AddCell(item.SoLuong.ToString());
                    table.AddCell(FormatCurrency((decimal)item.THUOC.Gia));
                    table.AddCell(FormatCurrency((decimal)(item.THUOC.Gia * item.SoLuong)));

                }

                // Thêm bảng vào tài liệu PDF
                document.Add(table);
                document.Add(new Paragraph("--------------------------------", infoFont));

                // Thêm tổng tiền
                document.Add(new Paragraph($"Medical Fee: {FormatCurrency((decimal)tempInvoice.TienKham)} ", infoFont));
                document.Add(new Paragraph("--------------------------------", infoFont));
                document.Add(new Paragraph($"Total: {FormatCurrency((decimal)tempInvoice.TongTien)} ", infoFont));
                document.Add(new Paragraph("--------------------------------", infoFont));
                // Thêm dòng cảm ơn
                document.Add(new Paragraph("Thank you!", infoFont));

                // Đóng tài liệu
                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lưu PDF: {ex.Message}");
            }
        }


        public string GenerateReceiptFromView(HOADON invoice)
        {
        var builder = new StringBuilder();

        builder.AppendLine("INVOICE");
        builder.AppendLine("--------------------------------");

        builder.AppendLine($"Customer Name: {invoice.PHIEUKHAM.BENHNHAN.TenBN}");
        builder.AppendLine($"Adress: {invoice.PHIEUKHAM.BENHNHAN.DiaChi}");
        builder.AppendLine($"Phone Number: {invoice.PHIEUKHAM.BENHNHAN.DienThoai}");
           
        builder.AppendLine($"Birthday: {invoice.PHIEUKHAM.BENHNHAN.NgaySinh:dd/MM/yyyy}");
        builder.AppendLine($"Gender: {invoice.PHIEUKHAM.BENHNHAN.GioiTinh}");

        builder.AppendLine($"Invoice Code: {invoice.MaHD}");
        builder.AppendLine($"Invoice Date: {invoice.NgayHD:dd/MM/yyyy}");
       
        builder.AppendLine("--------------------------------");
        builder.AppendLine("Medicine Name        Quantity    Unit Price      Price");
        builder.AppendLine("--------------------------------");

        foreach (var item in invoice.PHIEUKHAM.CTTTs)
        {
            builder.AppendLine($"{RemoveDiacritics(item.THUOC.TenThuoc).PadRight(12)} {item.SoLuong.ToString().PadRight(5)} {item.THUOC.Gia.ToString().PadRight(8)} {item.THUOC.Gia.ToString()}");
        }

        builder.AppendLine("--------------------------------");
            builder.AppendLine($"Total: {FormatCurrency((decimal)invoice.TongTien).ToString()}");

            builder.AppendLine("--------------------------------");
        builder.AppendLine("Thank you!");

        return builder.ToString();
}
       

        public void SavePosFile(string content, string filePath)
            {
                File.WriteAllText(filePath, content);
            }

   

        static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Chuẩn hóa chuỗi thành dạng FormD
            string normalizedString = text.Normalize(NormalizationForm.FormD);

            // Duyệt qua từng ký tự trong chuỗi
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) // Bỏ qua các ký tự dấu
                {
                    stringBuilder.Append(c);
                }
            }

            // Chuẩn hóa chuỗi thành FormC (khôi phục lại các ký tự ghép)
            string result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Thay thế thủ công các ký tự đặc biệt
            result = result.Replace("Đ", "D").Replace("đ", "d")
                   .Replace("á", "a").Replace("à", "a").Replace("ả", "a").Replace("ã", "a").Replace("ạ", "a")
                   .Replace("ă", "a").Replace("ắ", "a").Replace("ằ", "a").Replace("ẳ", "a").Replace("ẵ", "a").Replace("ặ", "a")
                   .Replace("â", "a").Replace("ấ", "a").Replace("ầ", "a").Replace("ẩ", "a").Replace("ẫ", "a").Replace("ậ", "a")
                   .Replace("é", "e").Replace("è", "e").Replace("ẻ", "e").Replace("ẽ", "e").Replace("ẹ", "e")
                   .Replace("ê", "e").Replace("ế", "e").Replace("ề", "e").Replace("ể", "e").Replace("ễ", "e").Replace("ệ", "e")
                   .Replace("í", "i").Replace("ì", "i").Replace("ỉ", "i").Replace("ĩ", "i").Replace("ị", "i")
                   .Replace("ó", "o").Replace("ò", "o").Replace("ỏ", "o").Replace("õ", "o").Replace("ọ", "o")
                   .Replace("ô", "o").Replace("ố", "o").Replace("ồ", "o").Replace("ổ", "o").Replace("ỗ", "o").Replace("ộ", "o")
                   .Replace("ơ", "o").Replace("ớ", "o").Replace("ờ", "o").Replace("ở", "o").Replace("ỡ", "o").Replace("ợ", "o")
                   .Replace("ú", "u").Replace("ù", "u").Replace("ủ", "u").Replace("ũ", "u").Replace("ụ", "u")
                   .Replace("ư", "u").Replace("ứ", "u").Replace("ừ", "u").Replace("ử", "u").Replace("ữ", "u").Replace("ự", "u")
                   .Replace("ý", "y").Replace("ỳ", "y").Replace("ỷ", "y").Replace("ỹ", "y").Replace("ỵ", "y");

                    return result;
            }
    }

    #endregion
}
   

