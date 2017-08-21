using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class ActualLevelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var actual = (ActualLevels)value;
            switch (actual)
            {
                case ActualLevels.Initial:
                    return "";

                case ActualLevels.Interlocking:
                    return ("IXL");

                case ActualLevels.Intermittent:
                    return ("ITC");

                case ActualLevels.Continuous:
                    return ("CTC");

                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}