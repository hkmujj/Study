using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class DriveModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (DriveModel)value;
            switch (model)
            {
                default:
                case DriveModel.None:
                    return ("");
                case DriveModel.ATO:
                    return ("AM");
                case DriveModel.Supervised:
                    return ("SM");
                case DriveModel.Restricted:
                    return ("RM");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}