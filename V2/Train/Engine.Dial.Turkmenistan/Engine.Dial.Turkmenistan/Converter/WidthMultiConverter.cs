using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Turkmenistan.Converter
{
    public class WidthMultiConverter : IMultiValueConverter
    {
        public WidthMultiConverter()
        {
            Ratio = 3d / 4d;
        }

        public double Ratio { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null) || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var width = (double)values[0];
            var height = (double)values[1];

            var temp = height / width;
            if (temp >= Ratio)
            {
                return width;
            }

            return height * 4 / 3;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}