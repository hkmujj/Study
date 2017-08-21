using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class ReverseModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (ReverseModel)value;
            switch (model)
            {
                default:
                case ReverseModel.Initial:
                    return (null);

                case ReverseModel.AROffered:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.AR_offered);

                case ReverseModel.ARActive:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.AR_active);

                case ReverseModel.DTRO:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.DTRO_is_offered);

                case ReverseModel.DTROactive:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.DTRO_active);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}