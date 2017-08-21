using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace General.CIR.Converter
{
    public class ProgressbarValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool arg_2C_0;
            if (values != null)
            {
                arg_2C_0 = values.Any(a => (a == null || a == DependencyProperty.UnsetValue));
            }
            else
            {
                arg_2C_0 = true;
            }
            bool flag = arg_2C_0;
            object result;
            if (flag)
            {
                result = DependencyProperty.UnsetValue;
            }
            else
            {
                double num = (double)values[1];
                double num2 = (double)values[2];
                double num3 = (double)values[0];
                bool flag2 = num3 >= num;
                if (flag2)
                {
                    result = 0;
                }
                else
                {
                    bool flag3 = num3 < num2;
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = num - num3;
                    }
                }
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
