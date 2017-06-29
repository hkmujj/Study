using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongRecTypeCConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (value == null || (value is string && ((string)value).IsNullOrWhiteSpace()))
            {
                return "";
            }
            return "C" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
