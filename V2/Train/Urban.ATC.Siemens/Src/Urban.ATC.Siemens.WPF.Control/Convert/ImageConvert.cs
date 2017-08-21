using System;
using System.Globalization;
using System.Windows.Data;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class ImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.RedRound);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}