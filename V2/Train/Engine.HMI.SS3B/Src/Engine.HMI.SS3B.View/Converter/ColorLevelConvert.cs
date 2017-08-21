using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Engine.HMI.SS3B.Interface.ViewState;

namespace Engine.HMI.SS3B.View.Converter
{
    class ColorLevelConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (ColorLevel) value;
            switch (tmp)
            {
                case ColorLevel.Green:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(Colors.Green);
                case ColorLevel.DarkGray:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(Colors.DarkGray);
                case ColorLevel.Red:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(Colors.Red);
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
