using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class TractionOrBrakeMulConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null ||values == DependencyProperty.UnsetValue|| values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var tmp = (float)values[0];
            var tmp1 = (float)values[1];


            if ((float)values[0] > 0.0)
            {
                return string.Format("{0:F0}%", (-tmp));
            }
            if ((float)values[1] > 0.0)
            {
                return string.Format("{0:F0}%", tmp1);
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}