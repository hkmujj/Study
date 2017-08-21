using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    public class PasswordConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var iPara = value;//System.Convert.ToInt64(value);
                if (parameter == null)
                {
                    return iPara;// == 0 ? string.Empty : iPara.ToString("F0");
                }
                

                var str = iPara.ToString();
                result = result.PadLeft(str.Length, System.Convert.ToChar(parameter.ToString()));
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}