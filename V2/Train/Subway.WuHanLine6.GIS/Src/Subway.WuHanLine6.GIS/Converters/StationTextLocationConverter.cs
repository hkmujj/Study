using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Subway.WuHanLine6.GIS.Converters
{
    public class StationTextLocationConverter : IMultiValueConverter
    {
        public StationTextLocationConverter()
        {
            Thicknesses = new Dictionary<char, Thickness>();
            alldic = new Dictionary<string, Dictionary<char, Thickness>>();
            Thicknesseses = new Dictionary<int, Thickness[]>();
        }
        private Dictionary<char, Thickness> Thicknesses { get; set; }


        private Dictionary<string, Dictionary<char, Thickness>> alldic { get; set; }
        private Dictionary<int, Thickness[]> Thicknesseses { get; set; }

        private double lastHeight = 0;
        /// <summary>
        /// 将源值转换为绑定源的值。数据绑定引擎在将值从绑定源传播给绑定目标时，调用此方法。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。<see cref="T:System.Windows.DependencyProperty"/>.<see cref="F:System.Windows.DependencyProperty.UnsetValue"/> 的返回值表示转换器没有生成任何值，且绑定将使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/>（如果可用），否则将使用默认值。<see cref="T:System.Windows.Data.Binding"/>.<see cref="F:System.Windows.Data.Binding.DoNothing"/> 的返回值表示绑定不传输值，或不使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> 或默认值。
        /// </returns>
        /// <param name="values"><see cref="T:System.Windows.Data.MultiBinding"/> 中源绑定生成的值的数组。值 <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> 表示源绑定没有要提供以进行转换的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return 0;
            }
            var value1 = (double)values[0];
            var value2 = values[1] as string;
            var value4 = (double)values[2];
            var panel = values[3] as StackPanel;
            var content = values[4] as ContentPresenter;

            var index = panel.Children.IndexOf(content);

            if (Thicknesseses.ContainsKey(value2.Length))
            {
                if (IsEmpty(Thicknesseses[value2.Length][index == 0 ? 0 : 1]))
                {
                    var result = GetResult(index, value1, value2.Length, value4);
                    Thicknesseses[value2.Length][index == 0 ? 0 : 1] = result;
                }

                return index == 0 ? Thicknesseses[value2.Length][0] : Thicknesseses[value2.Length][1];
            }
            else
            {
                var result = GetResult(index, value1, value2.Length, value4);

                Thicknesseses.Add(value2.Length, new Thickness[2]);

                Thicknesseses[value2.Length][index == 0 ? 0 : 1] = result;
                return result;
            }
        }
        private bool IsEmpty(Thickness thic)
        {
            return thic.Left == 0 && thic.Right == 0 && thic.Top == 0 && thic.Bottom == 0;
        }
        private static Thickness GetResult(int index, double value1, int len, double value4)
        {
            var result = new Thickness();
            if (index != -1)
            {
                if (index == 0)
                {
                    result = new Thickness(0);
                }
                else
                {

                    var height = ((value1 - value4) / (len - 1));
                    result = new Thickness(0, (height - value4), 0, 0);
                }
            }
            return result;
        }

        /// <summary>
        /// 将绑定目标值转换为源绑定值。
        /// </summary>
        /// <returns>
        /// 从目标值转换回源值的值的数组。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetTypes">要转换到的类型数组。数组长度指示为要返回的方法所建议的值的数量与类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StationTextLocationConverter2 : IMultiValueConverter
    {
        public StationTextLocationConverter2()
        {
            Thicknesses = new Dictionary<char, Thickness>();
            alldic = new Dictionary<string, Dictionary<char, Thickness>>();
            Thicknesseses = new Dictionary<int, Thickness[]>();
        }
        private Dictionary<char, Thickness> Thicknesses { get; set; }


        private Dictionary<string, Dictionary<char, Thickness>> alldic { get; set; }
        private Dictionary<int, Thickness[]> Thicknesseses { get; set; }

        private double lastHeight = 0;
        /// <summary>
        /// 将源值转换为绑定源的值。数据绑定引擎在将值从绑定源传播给绑定目标时，调用此方法。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。<see cref="T:System.Windows.DependencyProperty"/>.<see cref="F:System.Windows.DependencyProperty.UnsetValue"/> 的返回值表示转换器没有生成任何值，且绑定将使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/>（如果可用），否则将使用默认值。<see cref="T:System.Windows.Data.Binding"/>.<see cref="F:System.Windows.Data.Binding.DoNothing"/> 的返回值表示绑定不传输值，或不使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> 或默认值。
        /// </returns>
        /// <param name="values"><see cref="T:System.Windows.Data.MultiBinding"/> 中源绑定生成的值的数组。值 <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> 表示源绑定没有要提供以进行转换的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Any(a => a == null || a == DependencyProperty.UnsetValue))
            {
                return 0;
            }
            var value1 = (double)values[0];
            var value2 = values[1] as string;
            var value4 = (double)values[2];
            var panel = values[3] as StackPanel;
            var content = values[4] as ContentPresenter;

            var index = panel.Children.IndexOf(content);

            if (Thicknesseses.ContainsKey(value2.Length))
            {
                if (IsEmpty(Thicknesseses[value2.Length][index == 0 ? 0 : 1]))
                {
                    var result = GetResult(index, value1, value2.Length, value4);
                    Thicknesseses[value2.Length][index == 0 ? 0 : 1] = result;
                }

                return index == 0 ? Thicknesseses[value2.Length][0] : Thicknesseses[value2.Length][1];
            }
            else
            {
                var result = GetResult(index, value1, value2.Length, value4);

                Thicknesseses.Add(value2.Length, new Thickness[2]);

                Thicknesseses[value2.Length][index == 0 ? 0 : 1] = result;
                return result;
            }
        }
        private bool IsEmpty(Thickness thic)
        {
            return thic.Left == 0 && thic.Right == 0 && thic.Top == 0 && thic.Bottom == 0;
        }
        private static Thickness GetResult(int index, double value1, int len, double value4)
        {
            var result = new Thickness();
            if (index != -1)
            {
                if (index == 0)
                {
                    result = new Thickness(0);
                }
                else
                {

                    var height = ((value1 - value4) / (len - 1));
                    result = new Thickness(0, (height - value4), 0, 0);
                }
            }
            return result;
        }

        /// <summary>
        /// 将绑定目标值转换为源绑定值。
        /// </summary>
        /// <returns>
        /// 从目标值转换回源值的值的数组。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetTypes">要转换到的类型数组。数组长度指示为要返回的方法所建议的值的数量与类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}