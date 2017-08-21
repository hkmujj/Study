using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TPX21F.HXN5B.Converter
{
    class TargetDistanceFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var t = System.Convert.ToInt32(value);
            if (t <= 1000)
            {
                return t.ToString("0");
            }
            else
            {
                return ( ( t / 10 ) * 10 ).ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}