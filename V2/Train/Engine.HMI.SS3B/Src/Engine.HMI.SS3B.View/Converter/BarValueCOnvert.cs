using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Engine.HMI.SS3B.Interface;

namespace Engine.HMI.SS3B.View.Converter
{
    public class BarValueConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (IGraduationResouce)values[0];
            if (values[1] == DependencyProperty.UnsetValue)
            {
                return 0d;
            }
            return tmp.GetScal((double)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}