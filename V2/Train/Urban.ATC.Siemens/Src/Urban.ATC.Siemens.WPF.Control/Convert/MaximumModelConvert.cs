using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class MaximumModelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (MaximumMode)value;
            switch (model)
            {
                default:
                case MaximumMode.Initial:
                    return string.Empty;
                case MaximumMode.RestrictedMode:
                    return "RM";
                case MaximumMode.SMIntermittent:
                    return "SM-I";
                case MaximumMode.SMContinuous:
                    return "SM-C";
                case MaximumMode.AMIntermittent:
                    return "AM-I";
                case MaximumMode.AMContinuous:
                    return "AM-C";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}