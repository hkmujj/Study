using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Enum;

namespace Subway.ShenZhenLine11.Converter
{
    public class EnumConverterToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var ds1 = (System.Enum)value;
            return EnumUtil.GetDescription(ds1).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}