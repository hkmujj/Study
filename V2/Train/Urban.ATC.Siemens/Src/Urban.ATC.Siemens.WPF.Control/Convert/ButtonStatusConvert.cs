using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class ButtonStatusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (ButtonStatus)value;
            switch (status)
            {
                default:
                case ButtonStatus.Initial:
                    return null;

                case ButtonStatus.Disturbed:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Button_s__disturbed);

                case ButtonStatus.Reactivation:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Button_s__disturbed);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}