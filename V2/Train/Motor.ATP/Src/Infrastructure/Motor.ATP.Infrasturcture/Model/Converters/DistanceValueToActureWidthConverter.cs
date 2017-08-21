using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class DistanceValueToActureWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Any(a => a == DependencyProperty.UnsetValue) ||
                values.Length < 3
                || !(values[0] is IPlanSectionCoordinate)
                || !(values[1] is double)
                || !(values[2] is double || values[2] is float)
                )
            {
                return DependencyProperty.UnsetValue;
            }

            var or = (IPlanSectionCoordinate) values[0];
            var aw = (double) values[1];
            var dis = System.Convert.ToDouble(values[2]);

            // -4 减小误差。
            return or.GetDistanceScal(dis)*aw - 4;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}