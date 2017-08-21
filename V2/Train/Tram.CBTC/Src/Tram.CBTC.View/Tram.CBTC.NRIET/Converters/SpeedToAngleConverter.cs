using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Signal.Speed;

namespace Tram.CBTC.NRIET.Converters
{
    public class SpeedToAngleConverter : IMultiValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || !(values[0] is ISpeedDialPlate))
            {
                return DependencyProperty.UnsetValue;
            }
            return (double)((ISpeedDialPlate)values[0]).ConvertSpeedToAngle(System.Convert.ToSingle(values[1]));

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}