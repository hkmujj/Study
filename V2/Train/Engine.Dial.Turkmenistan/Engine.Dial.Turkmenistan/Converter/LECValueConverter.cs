using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Engine.Dial.Turkmenistan.Converter
{
    public class LEDValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var param = System.Convert.ToInt32(parameter.ToString());
            var tmp = ((double)value).ToString().Split('.')[0];
            var arry = tmp.ToCharArray();


            var tmpVAlue = string.Empty;

            foreach (var ch in arry)
            {
                tmpVAlue += ch.ToString();
                tmpVAlue += " ";
            }


            tmpVAlue = tmpVAlue.PadLeft(6, ' ');

            return tmpVAlue[param];

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}