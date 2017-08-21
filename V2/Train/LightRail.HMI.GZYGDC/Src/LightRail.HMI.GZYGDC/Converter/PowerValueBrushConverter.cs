using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LightRail.HMI.GZYGDC.Converter
{
    public class PowerValueBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var tmpValue = (float)value;
            if (tmpValue >= 0 && tmpValue <= 30)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if (tmpValue >= 31 && tmpValue <= 56)
            {
                return new SolidColorBrush(Colors.DarkOrange);
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(255, 100, 195, 61));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}