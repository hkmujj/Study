using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class StationConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==DependencyProperty.UnsetValue)
            {
                return "自动";
            }
            return EnumUtil.GetDescription((StationModel) value).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}