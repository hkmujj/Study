using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class ResourceKeyToResourceConverter : IMultiValueConverter
    {
        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values[1] || !(values[1] is string))
            {
                return DependencyProperty.UnsetValue;
            }
            var res = ((Control) values[0]).Resources;

            if (!res.Contains(values[1]))
            {
                return values[1];
            }
            return res[values[1]];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}