using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class DrawColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
            {
                var tem = (Color)value;
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(tem.A, tem.R, tem.G, tem.B));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}