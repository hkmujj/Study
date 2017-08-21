using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    internal class DepartureConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (DepartureType)value;
            switch (type)
            {
                case DepartureType.DoorCloseOrder:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Door_Close_Order);
                case DepartureType.DepartureRequest:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Departure_Request);
                case DepartureType.Hold:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.HOLD);
                case DepartureType.Skip:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.SKIP);
                case DepartureType.None:
                default:
                    return (null); ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}