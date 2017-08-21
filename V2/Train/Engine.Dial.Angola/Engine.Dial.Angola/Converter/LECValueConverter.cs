using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Angola.Converter
{
    public class LEDValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var tmp = (double)value;
            return tmp > 999 ? "888" : tmp.ToString("000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}