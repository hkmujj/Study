using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class DoorModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (DoorModel)value;
            switch (model)
            {
                default:
                case DoorModel.None:
                    return ("");
                case DoorModel.MM:
                    return ("MM");
                case DoorModel.AM:
                    return ("AM");
                case DoorModel.AA:
                    return ("AA");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}