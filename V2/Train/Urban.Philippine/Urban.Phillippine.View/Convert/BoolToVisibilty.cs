using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    public class BoolToVisibilty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                return ((bool)value) ? Visibility.Visible : Visibility.Hidden;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}