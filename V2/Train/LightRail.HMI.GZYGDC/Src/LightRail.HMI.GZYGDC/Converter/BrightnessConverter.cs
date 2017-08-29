using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LightRail.HMI.GZYGDC.Converter
{
    public class BrightnessConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue && value is int)
            {
                //最大透明度
                const int MAX_ALPHA = 200;

                var alpha = (int)((1 - System.Convert.ToDouble(value) / 100) * MAX_ALPHA);

                //ARGB
                return string.Format("#{0}000000", alpha.ToString("x"));
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
