using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tram.CBTC.NRIET.Converters
{
    public class CompareSpeedConverter : IMultiValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4 || values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var temp1 = (double)values[0];
            var temp2 = (float)values[1];

            if (temp1 < temp2)
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