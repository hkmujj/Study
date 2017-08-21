using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.NRIET.Converters
{
    public class BrakeDetailsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((BrakeDetailsType)value)
                {
                    case BrakeDetailsType.Normal:
                        return false;

                    case BrakeDetailsType.OverSpeed:
                        return true;

                    case BrakeDetailsType.EnmergencyBrake:
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