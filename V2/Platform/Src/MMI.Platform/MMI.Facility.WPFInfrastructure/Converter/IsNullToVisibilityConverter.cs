using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// 是否为空转化为是否可见
    /// </summary>
    public class IsNullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// null 时的值
        /// </summary>
        public Visibility VisibilityWhenNull { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public Visibility VisibilityWhenNotNull { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IsNullToVisibilityConverter()
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