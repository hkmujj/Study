using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.HMI.SS3B.View.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }
            var temp = (DateTime)value;
            if (temp == default(DateTime))
            {
                return string.Empty;
            }
            var bl = bool.Parse(parameter.ToString());
            if (bl)
            {
                return temp.ToString("yy-MM-dd");
            }
            return temp.ToString("hh:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}