using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Angola.Converter
{
    public class AngleConverter : IValueConverter
    {
        public double InitAngle { get; set; }

        public double MaxAngle { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var tmpValue = (double)value;
            if (tmpValue >= MaxValue)
            {
                return MaxAngle;
            }
            var sumAngle = Math.Abs(InitAngle) + Math.Abs(MaxAngle);

            var sumValue = MaxValue - MinValue;
            var result = (tmpValue / sumValue) * sumAngle;
            return result + InitAngle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}