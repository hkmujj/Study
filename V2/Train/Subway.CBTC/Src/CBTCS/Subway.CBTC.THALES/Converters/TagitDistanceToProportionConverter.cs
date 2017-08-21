using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Subway.CBTC.THALES.Converters
{
    public class TagitDistanceToProportionConverter : IValueConverter
    {
        private List<int> list = new List<int>();

        public TagitDistanceToProportionConverter()
        {
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(5);
            list.Add(10);
            list.Add(20);
            list.Add(50);
            list.Add(100);
            list.Add(200);
            list.Add(500);
            list.Add(1000);

        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var doublVlaue = (double)value;
            for (int i = 1; i < list.Count; ++i)
            {
                if (doublVlaue >= list[i - 1] && doublVlaue < list[i])
                {
                    return 0.1 * (i - 1) + (doublVlaue - list[i - 1]) / (list[i] - list[i - 1]) * 0.1;
                }
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
