using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;
using Subway.CBTC.BeiJiaoKong.Resources.Brushes;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongCabStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((CabState)value)
                {
                    case CabState.Actived:
                        return GDICommonColor.LightGreenBrush;

                    case CabState.StandBy:
                        return GDICommonColor.WhiteBrush;

                    case CabState.Unactived:
                        return GDICommonColor.BacgroundBrush;

                    case CabState.Fault:
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