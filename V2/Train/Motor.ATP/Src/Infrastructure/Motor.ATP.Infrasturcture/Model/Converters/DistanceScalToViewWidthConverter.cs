using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class DistanceScalToViewWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }

            return System.Convert.ToDouble(values[0]) * System.Convert.ToDouble(values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}