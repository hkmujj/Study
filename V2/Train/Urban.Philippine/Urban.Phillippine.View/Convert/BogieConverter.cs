using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class BogieConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                switch ((BogieStatus)value)
                {
                    case BogieStatus.BcuOffline:
                        return SolidColorBrushParam.OfflineBrush;

                    case BogieStatus.Separate:
                        return SolidColorBrushParam.RedBrush;

                    case BogieStatus.Fault:
                        return SolidColorBrushParam.RedBrush;

                    case BogieStatus.MechanicalBrakeApply:
                        return SolidColorBrushParam.IsolatedRed;

                    case BogieStatus.MechanicalBrakeRelease:
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

    public class BogieConvertToTest : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var temp = (BogieStatus)value;
                if (temp == BogieStatus.Separate)
                {
                    res = "×";
                }
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}