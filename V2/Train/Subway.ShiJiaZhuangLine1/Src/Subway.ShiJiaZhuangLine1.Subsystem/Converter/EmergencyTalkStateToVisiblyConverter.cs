using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class EmergencyTalkStateToVisiblyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if ((EmergencyTalkState)value == EmergencyTalkState.Null)
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}