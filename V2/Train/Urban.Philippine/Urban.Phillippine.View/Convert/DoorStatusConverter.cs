using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class DoorStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                switch ((DoorStatus)value)
                {
                    case DoorStatus.Offline:
                        return SolidColorBrushParam.OfflineBrush;

                    case DoorStatus.Isolated:
                        return SolidColorBrushParam.IsolatedRed;

                    case DoorStatus.Defective:
                        return SolidColorBrushParam.RedBrush;

                    case DoorStatus.Emergency:
                        return SolidColorBrushParam.Enmergency;

                    case DoorStatus.Obstade:
                        return SolidColorBrushParam.ObstacleBlue;

                    case DoorStatus.Open:
                        return SolidColorBrushParam.YellowBrush;

                    case DoorStatus.Close:
                        return SolidColorBrushParam.GreenBrush;

                    default:
                        throw new ArgumentOutOfRangeException(value.ToString(), value, null);
                }
            }
            return SolidColorBrushParam.DoorNormal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}