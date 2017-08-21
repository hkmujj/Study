using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Engine.TPX21F.HXN5B.Model.Interface;

namespace Engine.TPX21F.HXN5B.Converter
{
    class TargitDistanceToScalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Any(a => a == DependencyProperty.UnsetValue) || !( values[0] is ITargitDistanceScale ))
            {
                return DependencyProperty.UnsetValue;
            }

            return ( (ITargitDistanceScale)values[0] ).GetLocatoin(System.Convert.ToDouble(values[1])) * 100;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}