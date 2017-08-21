using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class DriveingBrakeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (DriveingBrakeType)value;
            switch (type)
            {
                default:
                case DriveingBrakeType.Initial:
                    return (null);
                case DriveingBrakeType.Motoring:

                    return (ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.motoring1));
                case DriveingBrakeType.Coasting:
                    return (ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.coasting));
                case DriveingBrakeType.Braking:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.braking);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}