using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine._6A.Converter
{
    public class DataTimeConvertToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != DependencyProperty.UnsetValue)
            {
                var tmp = (DateTime)value;
                result = string.Format("{0}-{1}-{2} {3}:{4}:{5}", tmp.Year, tmp.Month.ToString().PadLeft(2, '0'), tmp.Day.ToString().PadLeft(2, '0'), tmp.Hour.ToString().PadLeft(2, '0'), tmp.Minute.ToString().PadLeft(2, '0'), tmp.Second.ToString().PadLeft(2, '0'));
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}