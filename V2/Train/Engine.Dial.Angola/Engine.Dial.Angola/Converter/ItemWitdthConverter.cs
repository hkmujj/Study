using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Angola.Converter
{
    public class ItemWitdthConverter : IValueConverter
    {
        public double ItemNum { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return value;
            }
            var tmp = (double)value;

            return tmp / ItemNum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}