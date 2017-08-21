using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Subway.CBTC.THALES.Converters
{
    public class DialScaleTextColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var value1 = System.Convert.ToDouble(values[0]);
            var value2 = System.Convert.ToDouble(values[1]);
            if (value1 >= value2)
            {
                return values[2];
            }
            return values[3];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
