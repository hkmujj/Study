using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CommonUtil.Util;
using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class EnumToHelpDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Enum;
            if (v != null)
            {
                var att = v.GetAttibutes<HelpDescriptionAttribute>().FirstOrDefault();
                if (att != null)
                {
                    return att.Description;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enums = Enum.GetValues(targetType).OfType<Enum>().ToList();
            return enums.Find(f =>
            {
                var helpDescriptionAttribute = f.GetAttibutes<HelpDescriptionAttribute>().FirstOrDefault();
                return helpDescriptionAttribute != null && helpDescriptionAttribute.Description == (string)value;
            });
        }
    }
}