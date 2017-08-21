using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TAX2.SS7C.Converter
{
    class TargetDistanceScalLengthToLineTicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || !( value is double ))
            {
                return DependencyProperty.UnsetValue;
            }
            var t = System.Convert.ToDouble(value);
            if (Math.Abs(t - 1) < double.Epsilon)
            {
                return 2;
            }
            return 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}