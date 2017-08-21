using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class TrainVisibilityConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==DependencyProperty.UnsetValue)
            {
                return Visibility.Hidden;
            }
            return ((bool) value) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}