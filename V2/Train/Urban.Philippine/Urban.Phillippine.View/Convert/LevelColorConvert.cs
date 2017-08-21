using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class LevelColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var tmp = (LevelColor)value;
                if (tmp == LevelColor.Red)
                {
                    return SolidColorBrushParam.RedBrush;
                }
                else
                {
                    return SolidColorBrushParam.WhiteBrush;
                }
            }
            return SolidColorBrushParam.WhiteBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}