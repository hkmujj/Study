using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class TrainInteGrityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (TrainInteGrity)value;
            switch (type)
            {
                default:
                case TrainInteGrity.Initial:
                    return (null);
                case TrainInteGrity.TrainIntegrity:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Train_Integrity_not_ok);
                case TrainInteGrity.BrakingPressure:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.RST_Braking_pressure_not_ok);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}