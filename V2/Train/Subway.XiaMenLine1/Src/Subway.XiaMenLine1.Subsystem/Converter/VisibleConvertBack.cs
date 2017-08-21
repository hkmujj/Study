using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class VisibleConvertBack : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return Visibility.Visible;
            }
            return ((bool)value) ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}