using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// 条件选择内容转换器
    /// </summary>
    public class ConditionContentConverter : IValueConverter
    {
        /// <summary>
        /// If
        /// </summary>
        public ObjectContentPair If { set; get; }


        /// <summary>
        /// ElseIfs
        /// </summary>
        public List<ObjectContentPair> ElseIfs { set; get; }

        /// <summary>
        /// Else
        /// </summary>
        public ObjectContentPair Else { set; get; }

        /// <summary>
        /// ConditionContentConverter
        /// </summary>
        public ConditionContentConverter()
        {
            ElseIfs = new List<ObjectContentPair>();
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。 如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (If == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (If.Key.Equals(value))
            {
                return If.Content;
            }

            if (ElseIfs.Any())
            {
                foreach (var oc in ElseIfs)
                {
                    if (oc.Key.Equals(value))
                    {
                        return oc.Content;
                    }
                }
            }

            if (Else != null)
            {
                return Else.Content;
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。 如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}