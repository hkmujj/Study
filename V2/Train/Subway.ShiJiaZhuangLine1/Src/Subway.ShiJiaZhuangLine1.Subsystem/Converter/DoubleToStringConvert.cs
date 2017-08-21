using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class DoubleToStringConvert : IValueConverter
    {
        public string StringFormat { set; get; }

        public DoubleToStringConvert()
        {
            StringFormat = "0";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            if (value is double)
            {
                var v = (double) value;

                if (Math.Abs(v) < Double.Epsilon)
                {
                    return "0";
                }

                return (v).ToString(StringFormat);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value.ToString());
        }
    }
}