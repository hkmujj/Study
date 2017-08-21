using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Engine.LCDM.HDX2.Entity.Extension;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class EnumToResourceConverter : IMultiValueConverter
    {
        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values[1] || !(values[1] is Enum))
            {
                return DependencyProperty.UnsetValue;
            }
            var res = ((Control)values[0]).Resources;

            var key = ((Enum) values[1]).GetResourceKey();
            if (!res.Contains(key))
            {
                return values[1];
            }
            return res[key];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}