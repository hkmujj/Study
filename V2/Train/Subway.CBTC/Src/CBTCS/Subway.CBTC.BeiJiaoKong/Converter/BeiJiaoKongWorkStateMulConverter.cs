using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongWorkStateMulConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values || values.Length < 1 || (!(values[0] is WorkState) && !(values[1] is BrakeState)) )
            {
                return DependencyProperty.UnsetValue;
            }

            Array xArray= parameter as Array;

            if (values[1] != DependencyProperty.UnsetValue && values[1] != null)
            {
                if ((BrakeState)values[1] == BrakeState.NormalRacing)
                {
                    return xArray.GetValue(3);
                }
            }

            if (values[0] != DependencyProperty.UnsetValue && values[0] != null)
            {
                switch ((WorkState)values[0])
                {
                    case WorkState.Tow:
                        return xArray.GetValue(0);
                    case WorkState.Coasting:
                        return xArray.GetValue(1);
                    case WorkState.Brake:
                        return xArray.GetValue(2);
                    default:
                        return DependencyProperty.UnsetValue;
                }
            }

            return DependencyProperty.UnsetValue; ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}