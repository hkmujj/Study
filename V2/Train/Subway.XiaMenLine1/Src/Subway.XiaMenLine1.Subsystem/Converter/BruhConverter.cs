using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class BruhConverter : IMultiValueConverter
    {


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(a => a != null) && values.All(a => a != DependencyProperty.UnsetValue))
            {
                if ((bool)values[0] && (WorkModel)values[3] == WorkModel.EB)
                {
                    return values[2];
                }
                else
                {
                    return values[1];
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}