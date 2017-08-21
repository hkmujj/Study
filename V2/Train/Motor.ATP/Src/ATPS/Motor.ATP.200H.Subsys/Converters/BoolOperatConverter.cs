using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Motor.ATP._200H.Subsys.Converters
{
    public class BoolOperatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != DependencyProperty.UnsetValue && parameter is string)
            {
                if ((string)parameter == "&&")
                {
                    var bs = values.Where(w => w is bool).Cast<bool>();

                    return bs.All(a => a);
                }

                if ((string)parameter == "||")
                {
                    var bs = values.Where(w => w is bool).Cast<bool>();

                    return bs.All(a => !a);
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] {};
        }
    }
}