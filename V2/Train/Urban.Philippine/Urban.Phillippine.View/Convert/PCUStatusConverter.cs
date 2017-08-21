using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class PcuStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                switch ((PcuStatus)value)
                {
                    case PcuStatus.Offline:
                        return SolidColorBrushParam.OfflineBrush;

                    case PcuStatus.Fault:
                        return SolidColorBrushParam.RedBrush;

                    case PcuStatus.Normal:
                        return SolidColorBrushParam.GreenBrush;

                    default:
                        throw new ArgumentOutOfRangeException(value.ToString(), value, null);
                }
            }
            return SolidColorBrushParam.TransBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}