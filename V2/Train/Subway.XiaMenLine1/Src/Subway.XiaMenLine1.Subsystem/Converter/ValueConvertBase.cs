using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class ValueConvertBase : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}