using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    /// <summary>
    /// 距离坐标值 显示位置
    /// </summary>
    public class DistanceScalToTextViewWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }

            var percent = System.Convert.ToDouble(values[0]);
            if (Math.Abs(percent) < double.Epsilon)
            {
                return percent * System.Convert.ToDouble(values[1]);
            }
            if (Math.Abs(percent - 1) < double.Epsilon)
            {
                return System.Convert.ToDouble(values[1]) - 17;
            }
            return percent * System.Convert.ToDouble(values[1]) - 5;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}