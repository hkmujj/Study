using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.NRIET.Converters
{
    public class ReturnInfoVisibleConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((ReturnInfo)value)
                {
                    case ReturnInfo.None:
                        return false;

                    case ReturnInfo.CurrentStationReturn:
                        return true;

                    case ReturnInfo.NextStationReurn:
                        return true;
                    default:
                        return false;
                        //throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}