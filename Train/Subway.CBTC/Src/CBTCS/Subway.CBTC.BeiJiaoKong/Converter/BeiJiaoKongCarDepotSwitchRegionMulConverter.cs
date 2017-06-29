using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongCarDepotSwitchRegionMulConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values || values.Length < 2 || (!(values[0] is bool) && !(values[1] is bool) && !(values[2] is bool)))
            {
                return DependencyProperty.UnsetValue;
            }

            Array xArray= parameter as Array;

            if (values[0] != DependencyProperty.UnsetValue && (bool)values[0])
            {
                return xArray.GetValue(0);
            }
            else if (values[1] != DependencyProperty.UnsetValue && (bool)values[1])
            {
                return xArray.GetValue(1);
            }
            else if (values[2] != DependencyProperty.UnsetValue &&(bool)values[2])
            {
                return xArray.GetValue(2);
            }
            return DependencyProperty.UnsetValue; ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}