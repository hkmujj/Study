using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// 枚举描述转换
    /// </summary>
    public class EnumDescritionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                return EnumUtil.GetDescription((Enum)value).FirstOrDefault();
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
