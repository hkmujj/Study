using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;
using Other.ContactLine.JW4G.Model;

namespace Other.ContactLine.JW4G.Converter
{
    public class WorkStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            return EnumUtil.GetDescription((WorkStates)value).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}