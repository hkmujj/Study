using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.TCMS.LanZhou.Converter
{
    class FloatToTowBrakeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue && value is float && parameter is string)
            {
                var type = (string)parameter;

                var v = (float)value;

                if (type == "Tow" && v > 0)
                {
                    return v;
                }
                if (type == "Brake" && v < 0)
                {
                    return -v;
                }
                else
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
