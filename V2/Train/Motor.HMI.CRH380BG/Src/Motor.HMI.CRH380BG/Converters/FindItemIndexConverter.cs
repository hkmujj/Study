using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Motor.HMI.CRH380BG.Converters
{
    public class FindItemIndexConverter : IMultiValueConverter
    {
        /// <summary>将源值转换为绑定源的值。数据绑定引擎在将值从绑定源传播给绑定目标时，调用此方法。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。<see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 的返回值表示转换器没有生成任何值，且绑定将使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />（如果可用），否则将使用默认值。<see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> 的返回值表示绑定不传输值，或不使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> 或默认值。</returns>
        /// <param name="values">
        /// <see cref="T:System.Windows.Data.MultiBinding" /> 中源绑定生成的值的数组。值 <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 表示源绑定没有要提供以进行转换的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values != DependencyProperty.UnsetValue && values.Length >= 2 && values[1] is IList)
            {
                var it = values[0];
                var collection = (IList)values[1];

                if (it == null)
                {
                    return string.Empty;
                }

                var firstIndex = 0;
                if (parameter != null)
                {
                    firstIndex = System.Convert.ToInt32(parameter);
                }

                return (collection.IndexOf(it) + firstIndex).ToString();
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>将绑定目标值转换为源绑定值。</summary>
        /// <returns>从目标值转换回源值的值的数组。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetTypes">要转换到的类型数组。数组长度指示为要返回的方法所建议的值的数量与类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
