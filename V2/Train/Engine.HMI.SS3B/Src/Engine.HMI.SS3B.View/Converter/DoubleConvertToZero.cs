using System;
using System.Globalization;
using System.Windows.Data;

namespace Engine.HMI.SS3B.View.Converter
{
    public class DoubleConvertToZero:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp =System.Convert.ToDouble(value) ;
            return tmp.ToString("0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}