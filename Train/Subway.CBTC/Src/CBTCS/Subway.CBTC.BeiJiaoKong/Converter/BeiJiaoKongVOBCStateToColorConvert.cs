using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;
using Subway.CBTC.BeiJiaoKong.Resources.Brushes;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongVOBCStateToColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((VOBCState)value)
                {
                    case VOBCState.Normal:
                        return GDICommonColor.LightGreyBrush;

                    case VOBCState.PartialFault:
                        return GDICommonColor.OrangeBrush;

                    case VOBCState.CompleteFault:
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