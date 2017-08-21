using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Turkmenistan.Converter
{
    public class PointerAngleConverter : IValueConverter
    {
        public double MaxValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var tmpValue = (double)value;

            var x = tmpValue / MaxValue;
            return 270 * x - 45;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
