using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.HMI.SS3B.View.Converter
{
    public class TwoDoubleConvertThree : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue==values[0])
            {
                return "0.0 / 0.0";
            }
            return string.Format("{0} / {1}", ((double) values[0]).ToString("f1"), ((double) values[1]).ToString("f1"));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
