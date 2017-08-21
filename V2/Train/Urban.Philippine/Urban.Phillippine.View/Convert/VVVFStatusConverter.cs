using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class VVVFStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                switch ((VVVFStatus)value)
                {
                    case VVVFStatus.Offline:
                        return SolidColorBrushParam.OfflineBrush;

                    case VVVFStatus.Defective:
                        return SolidColorBrushParam.RedBrush;

                    case VVVFStatus.Normal:
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