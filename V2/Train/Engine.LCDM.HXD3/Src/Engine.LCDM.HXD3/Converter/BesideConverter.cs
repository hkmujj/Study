using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HXD3.Converter
{
    public class BesideConverter:IValueConverter
    {
        public double Beside { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null||value==DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            return ((double) value)/Beside;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}