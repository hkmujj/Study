using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class DoorOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a=> a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            if ((bool)values[0] && (Visibility)values[1] == Visibility.Visible)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}