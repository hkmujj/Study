using System;
using System.Globalization;
using System.Windows.Data;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class StringToDoubleNegative : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (!string.IsNullOrEmpty(str))
            {
                double x = double.Parse(str);
                x = -x;
                str = x.ToString();
            }
            return str;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
