using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class LineValueMappingConverter : IValueConverter
    {
        public double MinSourceValue { set; get; }

        public double MaxSourceValue { set; get; }

        public double MinTargetValue { set; get; }

        public double MaxTargetValue { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == value)
            {
                return MinTargetValue;
            }
            var d = (double) value;

            if (d <= MinSourceValue)
            {
                return MinTargetValue;
            }

            if (d >= MaxSourceValue)
            {
                return MaxTargetValue;
            }

            return (MaxTargetValue - MinTargetValue) / (MaxSourceValue - MinSourceValue) * d + MinTargetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}