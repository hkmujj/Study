using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class DivisionConveter : IValueConverter
    {
        /// <summary>
        /// 除数
        /// </summary>
        public double Divisor { set; get; }

        ///// <summary>
        ///// 被除数
        ///// </summary>
        //public double Dividend { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

        

            return ((double)value) / Divisor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
      
            return ((double)value) * Divisor;
        }
    }
}