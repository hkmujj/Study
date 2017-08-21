using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TCMS.HXD3.Converter
{
    /// <summary>
    /// 掩码转换
    /// </summary>
    public class MaskConverter : IValueConverter
    {
        /// <summary>
        /// 掩码
        /// </summary>
        public char Mask { get; set; }
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定源生成的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (Mask == default(char))
            {
                return value;
            }

            if (value.ToString().Length==0)
            {
                return DependencyProperty.UnsetValue;
            }
            return Mask.ToString().PadLeft(value.ToString().Length, Mask);
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetType">要转换到的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}