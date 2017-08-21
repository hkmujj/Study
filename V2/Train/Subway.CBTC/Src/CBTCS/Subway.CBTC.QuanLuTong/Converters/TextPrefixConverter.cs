using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;

namespace Subway.CBTC.QuanLuTong.Converters
{
    public class TextPrefixConverter :  IValueConverter
    {
        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !(parameter is string))
            {
                return DependencyProperty.UnsetValue;
            }
            var ss = ((string)parameter).Split('`');
            if (DependencyProperty.UnsetValue == value || value == null ||
                (value is string && ((string)value).IsNullOrWhiteSpace()))
            {
                return ss[1];
            }

            return ss[0] + value;

        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}