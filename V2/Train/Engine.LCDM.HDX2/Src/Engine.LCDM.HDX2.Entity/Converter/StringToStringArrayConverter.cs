using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class StringToStringArrayConverter : IValueConverter
    {
        public string[] Separator { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == value || value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var v = value as string;
            if (v != null)
            {
                return v.Split(Separator, StringSplitOptions.None);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}