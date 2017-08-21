using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class StopModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (StopModel)value;
            switch (model)
            {
                default:
                case StopModel.Initial:
                    return (null);
                case StopModel.Outside:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Outside_stopping_window);
                case StopModel.Inside:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Inside_Stopping_window);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}