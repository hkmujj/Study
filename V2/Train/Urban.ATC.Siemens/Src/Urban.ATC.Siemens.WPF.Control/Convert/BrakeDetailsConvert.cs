using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    internal class BrakeDetailsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (BrakeDetailsType)value;
            switch (type)
            {
                case BrakeDetailsType.Initial:
                    return GDICommonColor.BacgroundBrush;

                case BrakeDetailsType.BrakingRequired:
                    return GDICommonColor.OrangeBrush;

                case BrakeDetailsType.EnmergencyBrake:
                    return GDICommonColor.RedBrush;

                default:
                    return GDICommonColor.BacgroundBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}