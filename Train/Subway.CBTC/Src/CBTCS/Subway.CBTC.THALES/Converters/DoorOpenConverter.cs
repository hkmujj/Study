using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.THALES.Converters
{
    public class DoorOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var state0 = (DoorState)values[0];
            var state1 = (DoorState)values[1];
            if (state0 == DoorState.Opend || state1 == DoorState.Opend)
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
