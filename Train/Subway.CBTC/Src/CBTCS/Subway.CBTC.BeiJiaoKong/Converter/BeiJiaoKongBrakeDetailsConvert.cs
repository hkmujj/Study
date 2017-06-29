using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;
using Subway.CBTC.BeiJiaoKong.Resources.Brushes;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongBrakeDetailsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((BrakeDetailsType)value)
                {
                    case BrakeDetailsType.Normal:
                        return GDICommonColor.BacgroundBrush;

                    case BrakeDetailsType.OverSpeed:
                        return GDICommonColor.OrangeBrush;

                    case BrakeDetailsType.EnmergencyBrake:
                        return GDICommonColor.RedBrush;
                    default:
                        return GDICommonColor.BacgroundBrush;
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