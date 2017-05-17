using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CommonUtil.Util;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Enum;
            if (v != null)
            {
                return EnumUtil.GetDescription(v).FirstOrDefault();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enums = Enum.GetValues(targetType).OfType<Enum>().ToList();
            return enums.Find(f => EnumUtil.GetDescription(f).FirstOrDefault() == (string) value);
        }
    }
}
