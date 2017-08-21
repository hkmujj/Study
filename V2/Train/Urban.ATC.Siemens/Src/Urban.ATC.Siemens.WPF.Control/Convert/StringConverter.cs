using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                if (value != null)
                {
                    return (string.IsNullOrEmpty(value.ToString()) || value.ToString().Equals("0")) ? "?" : value.ToString();
                }
                else
                {
                    return "?";
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}