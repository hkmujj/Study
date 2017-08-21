using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class SelectConvertOne : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue)
            {
                if ((bool)values[0])
                {
                    return values[1];
                }
                else
                {
                    return values[2];
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}