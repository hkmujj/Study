using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class InfoLevelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = (InfoLevl)value;
            switch (level)
            {
                default:
                case InfoLevl.Yellow:
                    return GDICommonColor.YellowBrush;

                case InfoLevl.Red:
                    return GDICommonColor.RedBrush;
                case InfoLevl.Green:
                    return GDICommonColor.LightGreenBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}