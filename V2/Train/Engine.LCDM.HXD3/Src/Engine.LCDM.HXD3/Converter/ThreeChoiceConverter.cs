using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HXD3.Converter
{
    public class ThreeChoiceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(a => a == null) || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }

            var str = values[0].ToString().Split(';');
            var value = (string)values[1];
            switch (value)
            {
                case "100":
                    return str[0];
                case"010":
                    return str[1];
                case"001":
                    return str[2];
                default:
                    return str[0];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}