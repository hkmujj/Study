using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    /// <summary>
    /// 坡度中的距离转换成矩形宽度。
    /// </summary>
    public class GradientDistanceToRectWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Any(a => a == DependencyProperty.UnsetValue) ||
               values.Length != 4
               || !(values[0] is IPlanSectionCoordinate)
               || !(values[1] is double)
               || !(values[2] is float
               || !(values[3] is float)
               ))
            {
                return DependencyProperty.UnsetValue;
            }


            var or = (IPlanSectionCoordinate)values[0];
            var aw = (double)values[1];
            var dis = (float)values[2];
            var dis2 = (float) values[3];

            return or.GetDistanceScal(dis) * aw - or.GetDistanceScal(dis2) * aw; ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}