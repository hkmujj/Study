using Engine.Angola.TCMS.Model.MainData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Engine.Angola.TCMS.Converter
{
    public class LocaliadaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lococolor = (LocaliadaColor)value;
            switch (lococolor)
            {
                default:
                case LocaliadaColor.IndianRed:
                    return GDICommonColor.IndianRedBrush;

                case LocaliadaColor.Lime:
                    return GDICommonColor.LimeBrush;

                case LocaliadaColor.Cyan:
                    return GDICommonColor.CyanBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
