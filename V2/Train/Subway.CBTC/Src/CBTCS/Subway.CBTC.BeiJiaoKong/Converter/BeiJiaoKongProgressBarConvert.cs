using System;
using System.Globalization;
using System.Windows.Data;
using Subway.CBTC.BeiJiaoKong.Models.RegionA;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongProgressBarConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (double)value;
            return 1 - GraduationResouce.Instance.GetScal(tmp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}