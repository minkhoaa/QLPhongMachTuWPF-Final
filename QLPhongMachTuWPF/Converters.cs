using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QLPhongMachTuWPF
{
    public  class Converters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int status)
            {
                return status == 1 ? "Available" : "Unavailable";
            }
            return "Unknown";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Chuyển ngược lại, nếu cần
            if (value is string status)
            {
                return status == "Available" ? 1 : 0;
            }
            return 0;
        }
    }
}
