using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using QLPhongMachTuWPF.Model;

namespace QLPhongMachTuWPF.ViewModel
{
    public class HomepageVM : ViewModelBase
    {
        // Thuộc tính dữ liệu
        private ChartValues<int> _customerCounts;
        private List<string> _dayLabels;

        public ChartValues<int> CustomerCounts
        {
            get { return _customerCounts; }
            set { _customerCounts = value; OnPropertyChanged(); }
        }

        public List<string> DayLabels
        {
            get { return _dayLabels; }
            set { _dayLabels = value; OnPropertyChanged(); }
        }

        // Lệnh để tải dữ liệu
        public ICommand LoadChartDataCommand { get; set; }

        public HomepageVM()
        {
            // Khởi tạo lệnh
            LoadChartDataCommand = new RelayCommand<object>(CanLoadChartData, LoadChartData);

            
            // Khởi tạo giá trị mặc định cho các thuộc tính
            CustomerCounts = new ChartValues<int>();
            DayLabels = new List<string>();
        }

        private bool CanLoadChartData(object parameter)
        {
            return true; // Có thể tải dữ liệu nếu cần thiết
        }

        // Hàm tải dữ liệu và cập nhật biểu đồ
        private void LoadChartData(object parameter)
        {
            var (startOfWeek, endOfWeek) = GetCurrentWeekRange();
            var customerCounts = GetCustomerCountsPerDay(startOfWeek, endOfWeek);

            // Cập nhật dữ liệu biểu đồ
            CustomerCounts.Clear();
            DayLabels.Clear();

            for (int i = 0; i < 5; i++) // Từ Thứ Hai đến Thứ Sáu
            {
                var date = startOfWeek.AddDays(i);
                DayLabels.Add(date.ToString("dd/MM"));
                CustomerCounts.Add(customerCounts.ContainsKey(date) ? customerCounts[date] : 0);
            }
        }

        // Hàm truy vấn số lượng khách hàng từ cơ sở dữ liệu
        private Dictionary<DateTime, int> GetCustomerCountsPerDay(DateTime startOfWeek, DateTime endOfWeek)
        {
            var customerCounts = DataProvider.Ins.db.PHIEUKHAMs
                .Where(pk => pk.NgayKham >= startOfWeek && pk.NgayKham < endOfWeek) // Lọc các ngày khám trong tuần
                .GroupBy(pk => DbFunctions.TruncateTime(pk.NgayKham)) // Group theo ngày (TruncateTime)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToDictionary(g => g.Date.Value, g => g.Count); // Chuyển thành Dictionary

            return customerCounts;
        }

        // Hàm tính ngày đầu tuần (Thứ Hai) và cuối tuần (Thứ Sáu)
        private (DateTime, DateTime) GetCurrentWeekRange()
        {
            DateTime today = DateTime.Today;
            int daysToMonday = ((int)today.DayOfWeek + 6) % 7;
            DateTime startOfWeek = today.AddDays(-daysToMonday);
            DateTime endOfWeek = startOfWeek.AddDays(5); // Thứ Sáu là ngày cuối tuần
            return (startOfWeek, endOfWeek);
        }
    }
}
