using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TCMS.Turkmenistan.Converter
{
    public class DoubleToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null||value==DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (value.GetType()==typeof(double))
            {
                var tmp = (double)value;
                return tmp.ToString("F0");
            }
            return value;



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}