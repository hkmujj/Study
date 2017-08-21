using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class RecTypeCConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var tmp = (double)value;
            if (tmp > Double.Epsilon)
            {
                return "C" + tmp.ToString("000");
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}