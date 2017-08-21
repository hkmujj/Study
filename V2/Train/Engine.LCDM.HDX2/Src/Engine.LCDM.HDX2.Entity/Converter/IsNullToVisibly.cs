using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class IsNullToVisibility : IValueConverter
    {
        /// <summary>
        /// null 时的值
        /// </summary>
        public Visibility VisibilityWhenNull { set; get; }
        public Visibility VisibilityWhenNotNull { set; get; }

        public IsNullToVisibility()
        {
            VisibilityWhenNull = Visibility.Hidden;
            VisibilityWhenNotNull = Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return VisibilityWhenNull;
            }
            if (value == null)
            {
                return VisibilityWhenNull;
            }

            return VisibilityWhenNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}