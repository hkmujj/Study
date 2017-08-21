using System;
using System.Globalization;
using System.Windows.Data;
using Engine.HMI.SS3B.Interface.ViewState;

namespace Engine.HMI.SS3B.View.Converter
{
    public class NetPageColorConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (NetPageColor) value;
            switch (tmp)
            {
                case NetPageColor.None:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(ColorSolidBrushMgr.NetPageColor2);
                case NetPageColor.Normal:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(ColorSolidBrushMgr.NetPageColor1);
                case NetPageColor.Abnormity:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(ColorSolidBrushMgr.NetPageColor3);
                default:
                    return ColorSolidBrushMgr.Instance.GetSolidColorBrush(ColorSolidBrushMgr.NetPageColor2);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}