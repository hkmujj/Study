using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urban.Phillippine.View.Convert
{
    public class UnitConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var slip = value.ToString().Split('.');
                if (slip.Length > 0)
                {
                    var str = slip[0];
                    if (parameter == null)
                    {
                        result = str;
                    }
                    else
                    {
                        result = str + parameter;
                    }
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}