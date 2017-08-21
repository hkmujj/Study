using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.NRIET.Converters
{
    public class LineRunDirectionConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                //true为上行，false为下行
                if ((string)parameter == "true")
                {
                    return ((LineRunDirection)value) == LineRunDirection.Up;
                }
                else
                {
                    return ((LineRunDirection)value) == LineRunDirection.Down;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                //true为上行，false为下行
                if ((string)parameter == "true")
                {
                    return LineRunDirection.Up;
                }
                else
                {
                    return LineRunDirection.Down;
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}