using Engine.Angola.TCMS.Model.SWData;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Engine.Angola.TCMS.Converter
{
    public class Color5Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color5)value;
            switch (color)
            {
                default:
                case Color5.Green:
                    return GDICommonColor.GreenBrush;

                case Color5.Yellow:
                    return GDICommonColor.YellowBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
