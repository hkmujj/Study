using Engine.Angola.TCMS.Model.SWData;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Engine.Angola.TCMS.Converter
{
    public class Color10Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color10)value;
            switch (color)
            {
                default:
                case Color10.Green:
                    return GDICommonColor.GreenBrush;

                case Color10.Yellow:
                    return GDICommonColor.YellowBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
