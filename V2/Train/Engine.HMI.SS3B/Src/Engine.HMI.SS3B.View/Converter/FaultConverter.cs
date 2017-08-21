using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.HMI.SS3B.View.Converter
{
    public class FaultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}