using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tram.CBTC.NRIET.Converters
{
    public class DegreeScaleTextAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            //var v = System.Convert.ToDouble(value);
            //return -v;
            
            //逆时针旋转90度
            return -90;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}