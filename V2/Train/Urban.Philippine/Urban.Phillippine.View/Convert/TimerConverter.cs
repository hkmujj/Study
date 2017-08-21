using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    public class TimerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var s = (DateTime)value;
                result = string.Format("{0}:{1}:{2}", s.Hour.ToString().PadLeft(2, '0'),
                    s.Minute.ToString().PadLeft(2, '0'), s.Second.ToString().PadLeft(2, '0'));
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}