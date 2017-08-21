using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.ATC.Domain.Interface;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class TagertBarTypeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((TargetBarType)value)
                {
                    case TargetBarType.LightGreen:
                        return GDICommonColor.LightGreenBrush;

                    case TargetBarType.Yellow:
                        return GDICommonColor.YellowBrush;

                    case TargetBarType.Red:
                        return GDICommonColor.RedBrush;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
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