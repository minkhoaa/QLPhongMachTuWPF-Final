using System;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace QLPhongMachTuWPF.ViewModel
{
    public class HomepageVM : INotifyPropertyChanged
    {
        private SeriesCollection _chartData;
        private string[] _timeLabels;
        private string _selectedTimeline;

        public HomepageVM()
        {
            // Dummy data for the chart
            ChartData = new SeriesCollection
            {
                new ColumnSeries { Title = "Patients", Values = new ChartValues<int> { 10, 20, 30, 40, 50, 60, 70 } }
            };

            TimeLabels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            

            // Default timeline is "This week"
        }

        public SeriesCollection ChartData
        {
            get => _chartData;
            set
            {
                _chartData = value;
                OnPropertyChanged(nameof(ChartData));
            }
        }

        public string[] TimeLabels
        {
            get => _timeLabels;
            set
            {
                _timeLabels = value;
                OnPropertyChanged(nameof(TimeLabels));
            }
        }

        public string SelectedTimeline
        {
            get => _selectedTimeline;
            set
            {
                _selectedTimeline = value;
                OnPropertyChanged(nameof(SelectedTimeline));
                // Không cần cập nhật dữ liệu khi thay đổi SelectedTimeline
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
