using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Convert
{
    public class LevelConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                var tmp = (TractionBrakeLevel)value;
                if (tmp == TractionBrakeLevel.Unknow)
                {
                    result = "0";
                }
                else
                {
                    result = tmp.ToString();
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}