using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class SpecialModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (SpecialModel)value;
            switch (model)
            {
                default:
                case SpecialModel.Initial:
                    return (null);
                case SpecialModel.DepotEntry:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Depot_Entry);
                case SpecialModel.OnDepot:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Depot_Driver);
                case SpecialModel.ReleaseSpeed:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Relesase_Speed);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}