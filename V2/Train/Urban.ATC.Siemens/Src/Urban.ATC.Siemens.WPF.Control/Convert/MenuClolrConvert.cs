using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class MenuClolrConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (MenuColorTyep)value;
            switch (type)
            {
                default:
                case MenuColorTyep.Active:
                    return (GDICommonColor.LightGreyBrush);
                case MenuColorTyep.Invalid:
                    return (GDICommonColor.DarkGreyBrush);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}