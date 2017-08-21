using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class EmergenceTimeToShowFormatConverter  : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == value)
            {
                return DependencyProperty.UnsetValue;
            }

            var v = (int) value;

            if (v <= 0)
            {
                return string.Empty;
            }
            return v.ToString(":#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}