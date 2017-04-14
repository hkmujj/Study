using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Grid;

namespace MMI.Tester.BatchDataSender.Converter
{
    public class GetColumnIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || value == null)
            {
                return -1;
            }
            var c = ((GridColumn)value);

            return c.VisibleIndex;
        }
    }

}