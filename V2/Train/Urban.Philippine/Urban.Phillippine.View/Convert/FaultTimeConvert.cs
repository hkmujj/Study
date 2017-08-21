using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    public class FaultTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var tmp = (DateTime)value;
                if (tmp == default(DateTime))
                {
                    return string.Empty;
                }

                return string.Format("{0}-{1}-{2}\r\n {3}:{4}:{5}",tmp.Year,tmp.Month.ToString("00"),tmp.Date.ToString("00"),tmp.Hour.ToString("00"),tmp.Minute.ToString("00"),tmp.Second.ToString("00"));
            }


            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}