using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class ResourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(a => a == null) || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }

            var str = values[0].ToString().Split(';');
            var value = (bool)values[1];
            var result = value ? str[1] : str[0];
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
