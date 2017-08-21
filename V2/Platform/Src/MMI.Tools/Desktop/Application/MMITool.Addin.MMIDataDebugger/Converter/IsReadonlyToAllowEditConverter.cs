using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Utils;

namespace MMITool.Addin.MMIDataDebugger.Converter
{
    public class IsReadonlyToAllowEditConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                if ((bool)value)
                {
                    return DefaultBoolean.False;
                }
                return DefaultBoolean.True;
            }

            return DefaultBoolean.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}