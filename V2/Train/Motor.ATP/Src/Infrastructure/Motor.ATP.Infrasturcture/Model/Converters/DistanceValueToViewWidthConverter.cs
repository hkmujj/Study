using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class DistanceValueToViewWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 3 || !(values[0] is IPlanSectionCoordinate) || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var coor = (IPlanSectionCoordinate)values[0];
            return coor.GetDistanceScal(System.Convert.ToDouble(values[1])) * System.Convert.ToDouble(values[2]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}