using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class StringArrayGetterConverter : IValueConverter
    {
        public int Index { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == value || value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            var arr = value as string[];
            if (arr != null)
            {
                if (arr.Length > Index)
                {
                    return arr[Index];
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}