using System;
using System.Globalization;
using System.Windows.Data;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class ProgressBarConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (double)value;
            return 1 - GraduationResouce.Instance.GetScal(tmp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}