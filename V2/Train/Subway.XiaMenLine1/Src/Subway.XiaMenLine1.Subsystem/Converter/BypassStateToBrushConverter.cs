using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Subway.XiaMenLine1.Subsystem.Model;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class BypassStateToBrushConverter : IValueConverter
    {
        public Brush SwitchOnBrush { set; get; }

        public Brush SwitchOffBrush { set; get; }

        public Brush FaultBrush { set; get; }

        public Brush UnkownBrush { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            switch ((BypassState)value)
            {
                case BypassState.SwitchOn:
                    return SwitchOnBrush;
                case BypassState.SwitchOff:
                    return SwitchOffBrush;
                case BypassState.Fault:
                    return FaultBrush;
                case BypassState.Unknown:
                    return UnkownBrush;
                default:
                    throw new ArgumentOutOfRangeException("value", value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}