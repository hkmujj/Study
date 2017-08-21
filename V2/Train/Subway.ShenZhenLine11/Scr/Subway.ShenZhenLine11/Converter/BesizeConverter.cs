using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShenZhenLine11.Converter
{
    public class BesizeConverter : IValueConverter
    {
        /// <summary>
        /// 除数
        /// </summary>
        public double Besize { get; set; }
        /// <summary>
        /// 偏移
        /// </summary>
        public double Offset { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return value;
            }
            var tmp = ((double)value) - Offset;
            if (tmp < double.Epsilon)
            {
                return 0;
            }
            var of = parameter == null ? (0) : double.Parse(parameter.ToString());
            return (tmp / Besize) - of;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}