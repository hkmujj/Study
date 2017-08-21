using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class WorkModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return "";
            }
            return EnumUtil.GetDescription((WorkModel)value).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}