using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class OBCUModelConvert : IValueConverter
    {
        public Brush Level1Brush { set; get; }

        public Brush Level2Brush { set; get; }

        public Brush Level3Brush { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (OBCUModel)value;
            switch (model)
            {
                default:
                case OBCUModel.None:
                    return (null);
                case OBCUModel.Level1:
                    return Level1Brush;

                case OBCUModel.Level2:
                    return Level2Brush;

                case OBCUModel.Level3:
                    return Level3Brush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}