using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Extend;

namespace Urban.Phillippine.View.Convert
{
    public class UnitConverterToF2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var slip = value.ToString().ConvertToDouble();

                if (parameter == null)
                {
                    result = slip.ToString("F2");
                }
                else
                {
                    result = slip.ToString("F2") + parameter;
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