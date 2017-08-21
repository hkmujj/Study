using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.HMI.SS3B.View.Converter
{
    public class TwoDoubleConvertOne:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0]==DependencyProperty.UnsetValue)
            {
                return "0/0";
            }
            return string.Format("{0}/{1}", ((double)values[0]).ToString("f0"), ((double)values[1]).ToString("f0"));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}