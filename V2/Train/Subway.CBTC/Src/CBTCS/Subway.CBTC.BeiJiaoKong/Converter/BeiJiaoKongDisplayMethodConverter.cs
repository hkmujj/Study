using System;
using System.Globalization;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;
using Subway.CBTC.BeiJiaoKong.Resources.Brushes;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongDisplayMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var method = (InformationShowType)value;
            switch (method)
            {
                case InformationShowType.YellowFrameFlick:
                    return GDICommonColor.YellowBrush;

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
