using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// 乘法
    /// </summary>
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
            if (value != DependencyProperty.UnsetValue)
            {
                if (Math.Abs(Multiplier) > float.Epsilon)
                {
                    return System.Convert.ToDouble(value)/Multiplier;
                }
                return float.NaN;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}