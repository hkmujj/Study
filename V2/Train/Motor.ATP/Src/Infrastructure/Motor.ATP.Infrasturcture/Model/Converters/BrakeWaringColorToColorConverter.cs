using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class BrakeWaringColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || value.GetType() != typeof (ATPColor))
            {
                return DependencyProperty.UnsetValue;
            }
            var ac = (ATPColor) value;
            switch (ac)
            {
                case ATPColor.None:
                    return DependencyProperty.UnsetValue;
                case ATPColor.LightGrey:
                    return Brushes.LightGray;
                case ATPColor.Grey:
                    return Brushes.Gray;
                case ATPColor.Yellow:
                    return Brushes.Yellow;
                case ATPColor.Orange:
                    return Brushes.Orange;
                case ATPColor.Red:
                    return Brushes.Red;
                case ATPColor.White:
                    return Brushes.White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
