using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class TimeConverter : IValueConverter
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
            if (value != DependencyProperty.UnsetValue && value is DateTime)
            {
                var time = (DateTime)value;

                if (parameter != null && parameter is string)
                {
                    if ((string)parameter == "1")
                    {
                        return time.ToString("yyyy - MM - dd");
                    }
                    else if ((string) parameter == "2")
                    {
                        return time.ToString("HH : mm : ss");
                    }
                }
                else
                {
                    return string.Format("{0} {1}", time.ToString("yyyy - MM - dd"), time.ToString("HH:mm:ss"));
                }
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
