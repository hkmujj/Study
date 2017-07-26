using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class CTCSTypeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || value.GetType() != typeof(CTCSType))
            {
                return DependencyProperty.UnsetValue;
            }
            var ctc = (CTCSType) value;
            return EnumUtil.GetDescription(ctc).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}