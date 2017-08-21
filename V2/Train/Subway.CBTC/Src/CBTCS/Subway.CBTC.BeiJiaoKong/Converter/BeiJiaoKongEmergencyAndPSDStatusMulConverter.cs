using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongEmergencyAndPSDStatusMulConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 3 || (!(values[0] is RealTimeWokeStatus) && !(values[1] is DoorState) && !(values[2] is DoorState)))
            {
                return DependencyProperty.UnsetValue;
            }

            Array xArray = parameter as Array;

            if (values[0] != DependencyProperty.UnsetValue && (RealTimeWokeStatus)values[0] == RealTimeWokeStatus.EB)
            {
                    return xArray.GetValue(0);
            }

            if (values[1] != DependencyProperty.UnsetValue && values[2] != DependencyProperty.UnsetValue && (DoorState)values[1] == DoorState.Opend || (DoorState)values[2] == DoorState.Opend)
            {
                return xArray.GetValue(1);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}