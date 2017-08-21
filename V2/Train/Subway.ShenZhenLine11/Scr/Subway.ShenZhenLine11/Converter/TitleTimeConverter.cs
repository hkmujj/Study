using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShenZhenLine11.Converter
{
    public class TitleTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return value;
            }
            var tmp = (DateTime)value;
            return $"{tmp.Year}-{tmp.Month.ToString("00")}-{tmp.Day.ToString("00")}  {tmp.Hour.ToString("00")}:{tmp.Minute.ToString("00")}:{tmp.Second.ToString("00")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}