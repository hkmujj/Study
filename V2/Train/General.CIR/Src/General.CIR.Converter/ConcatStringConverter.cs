using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace General.CIR.Converter
{
    public class ConcatStringConverter : IMultiValueConverter
    {
        public string ConcatChar
        {
            get;
            set;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null) || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;

            }

            return (string.IsNullOrEmpty(ConcatChar) ? string.Concat(values) : (values[0] + ConcatChar + values[1]));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
