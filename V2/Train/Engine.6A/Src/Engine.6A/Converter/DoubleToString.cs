using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine._6A.Converter
{
    public class DoubleToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                return ((double)value).ToString("F0") + " kPa";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}