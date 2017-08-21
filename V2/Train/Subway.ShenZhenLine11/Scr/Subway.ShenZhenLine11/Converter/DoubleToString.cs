using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShenZhenLine11.Converter
{
    public class DoubleToString : IValueConverter
    {
        public string Digit { get; set; }

        public string Symbol { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var tmp = (double)value;
            return tmp.ToString(Digit) + Symbol;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}