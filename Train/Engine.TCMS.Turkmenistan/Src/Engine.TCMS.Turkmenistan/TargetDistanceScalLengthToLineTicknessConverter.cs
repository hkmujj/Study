using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TCMS.Turkmenistan
{
    class TargetDistanceScalLengthToLineTicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || !(value is double))
            {
                return DependencyProperty.UnsetValue;
            }
            var t = System.Convert.ToDouble(value);
            if (1 - t >= 0.5)
            {
                return 1;
            }
            return 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
