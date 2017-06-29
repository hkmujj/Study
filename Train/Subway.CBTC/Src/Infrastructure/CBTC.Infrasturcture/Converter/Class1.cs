using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CBTC.Infrasturcture.Converter
{
    public class LightPercentToBackColorConverter : IValueConverter
    {
        private const int MaxAlpha = 200;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var alpha = (int)((1 - (float)value / 100) * MaxAlpha);

            return string.Format("#{0}000000", alpha.ToString("x"));
            //#00FFFFFF
        }

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var alpha = 100;
            var s = string.Format("#{0}FFFFFF", alpha.ToString("x"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}