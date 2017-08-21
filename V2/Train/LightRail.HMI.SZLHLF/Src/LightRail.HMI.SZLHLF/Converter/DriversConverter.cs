using LightRail.HMI.SZLHLF.Model.TrainModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class DriversConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var direct1 = (Drivers)value;
            switch (direct1)
            {
                case Drivers.NotOccupy:
                    return GDICommonColor.WhiteBrush;

                case Drivers.Occupy:
                    return GDICommonColor.GreenBrush;

                default:
                    return GDICommonColor.WhiteBrush;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
