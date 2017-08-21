using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Converters
{
    public class CurrentInterfaceKeyToVisblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue != value && value is IDriverInterface && parameter is string)
            {
                var di = (IDriverInterface)value;
                if (di.Id.ToString() == (string) parameter)
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}