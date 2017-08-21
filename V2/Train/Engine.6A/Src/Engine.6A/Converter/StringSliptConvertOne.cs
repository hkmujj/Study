using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine._6A.Converter
{
    public class StringSliptConvertOne : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value != DependencyProperty.UnsetValue && value != null)
            {
                var slip = value.ToString().Split('#');
                if (slip.Length == 2)
                {
                    result = slip[0];
                }
            }
            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}