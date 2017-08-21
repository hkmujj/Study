using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Casco.Model.TempModel;

namespace Tram.CBTC.Casco.Converters
{
    public class SelectItemConverter : IValueConverter
    {
        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var tmpValue = value as ComboxSelectItemModel;

            if (tmpValue == null)
            {
                var tmp1 = value as ComboxSelectItemModel2;
                if (tmp1 == null)
                {
                    return string.Empty;
                }
                return tmp1.Content;
            }
            return tmpValue.Content;

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
