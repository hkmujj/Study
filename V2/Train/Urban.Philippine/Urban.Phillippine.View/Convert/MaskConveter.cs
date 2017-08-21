using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    /// <summary>
    /// 转换成掩码
    /// </summary>
    public class MaskConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {

                var tmp = value.ToString();
                if (tmp.Length <= 0)
                {
                    return "";
                }

                if (parameter == null)
                {
                    return "*".PadLeft(value.ToString().Length, '*');
                }
                else
                {
                    parameter.ToString().PadLeft(value.ToString().Length, System.Convert.ToChar(parameter));
                }


                //  return parameter?.ToString().PadLeft(value.ToString().Length, System.Convert.ToChar(parameter)) ?? "*".PadLeft(value.ToString().Length, '*');
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}