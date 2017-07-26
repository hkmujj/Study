using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface;
using System.Globalization;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class TrainIdConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 3 || values.Any(a => a == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            if (System.Convert.ToBoolean(values[2]) == true)
            {
                return values[0];
            }
            else
            {
                return values[1];
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
