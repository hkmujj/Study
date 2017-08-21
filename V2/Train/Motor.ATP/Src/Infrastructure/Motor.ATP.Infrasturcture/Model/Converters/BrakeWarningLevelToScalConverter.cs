using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class BrakeWarningLevelToScalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || value.GetType() != typeof(BrakeWarningLevel))
            {
                return DependencyProperty.UnsetValue;
            }
            var lel = (BrakeWarningLevel) value;
            switch (lel)
            {
                case BrakeWarningLevel.LevelUnkown:
                    return 0d;
                case BrakeWarningLevel.Level0:
                    return 0d;
                case BrakeWarningLevel.Level1:
                    return 1/4d;
                case BrakeWarningLevel.Level2:
                    return 1/2d;
                case BrakeWarningLevel.Level3:
                    return 3/4d;
                case BrakeWarningLevel.LevelFull:
                    return 1d;
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
