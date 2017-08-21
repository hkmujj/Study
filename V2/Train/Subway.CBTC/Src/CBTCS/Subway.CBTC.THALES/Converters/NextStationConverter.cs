using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.THALES.Converters
{
    public class NextStationConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var tempStation = (string)values[0];
            var tempDoorOpen = (NextStationDoorOpenDirection)values[1];
            if (string.IsNullOrEmpty(tempStation) || tempStation.Equals("---"))
            {
                return values[0];
            }
            if (tempDoorOpen == NextStationDoorOpenDirection.Left)
            {
                return "< " + tempStation;
            }
            else if (tempDoorOpen == NextStationDoorOpenDirection.Right)
            {
                return tempStation + " >";
            }
            else if (tempDoorOpen == NextStationDoorOpenDirection.Broth)
            {
                return "< " + tempStation + " >";
            }
            return tempStation;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
