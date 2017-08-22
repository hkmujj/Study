using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LightRail.HMI.GZYGDC.Model.State;


namespace LightRail.HMI.GZYGDC.Converter
{
    public class WindSpeedModeToBoolConverter : IValueConverter
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
            if (value != DependencyProperty.UnsetValue && value is WindSpeedMode)
            {
                if (parameter is WindSpeedMode)
                {
                    return (WindSpeedMode)parameter == (WindSpeedMode)value;
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
            if (parameter is WindSpeedMode)
            {
                return (WindSpeedMode)parameter;
            }

            WindSpeedMode tar;
            Enum.TryParse(parameter.ToString(), out tar);

            if ((bool)value)
            {
                return tar;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}