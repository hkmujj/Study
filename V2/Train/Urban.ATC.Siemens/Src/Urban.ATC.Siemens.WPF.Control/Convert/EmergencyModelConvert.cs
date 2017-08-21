using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class EmergencyModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (EmergencyModel)value;
            switch (model)
            {
                default:
                case EmergencyModel.None:
                    return (null);
                case EmergencyModel.Slip:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Slip_or_Slide);
                case EmergencyModel.EmergencyBrake:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Emergency_Brake);
                case EmergencyModel.PSDNotCLose:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.PSD_not_closed);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}