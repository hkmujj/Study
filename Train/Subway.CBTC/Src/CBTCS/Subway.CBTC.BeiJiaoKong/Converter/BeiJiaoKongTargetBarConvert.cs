using System;
using System.Globalization;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;
using Subway.CBTC.BeiJiaoKong.Resources.Brushes;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    internal class BeiJiaoKongTargetBarConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (CBTCColor)value;
            switch (type)
            {
                case CBTCColor.Gray:
                    return GDICommonColor.WhiteBrush;

                case CBTCColor.Yellow:
                    return GDICommonColor.YellowBrush;

                case CBTCColor.Green:
                    return GDICommonColor.LightGreenBrush;

                case CBTCColor.Red:
                    return GDICommonColor.RedBrush;

                default:
                    return GDICommonColor.BacgroundBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
