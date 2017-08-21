using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    internal class DoorDetailsModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mdoel = (ErrorModel)value;
            switch (mdoel)
            {
                default:
                case ErrorModel.None:
                    return (null);
                case ErrorModel.RAD:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.No_WCURadio_link);
                case ErrorModel.ATP:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.ATP_fault);
                case ErrorModel.ATO:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.ATO_fault);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}