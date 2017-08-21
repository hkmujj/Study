using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class MultiplicationConverter : IValueConverter
    {
        /// <summary>
        /// 乘数
        /// </summary>
        public double Multiplier { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                return System.Convert.ToDouble(value)*Multiplier;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}