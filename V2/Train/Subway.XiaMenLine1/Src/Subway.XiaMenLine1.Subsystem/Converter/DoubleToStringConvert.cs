using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class DoubleToStringConvert : IValueConverter
    {
        public string StringFormat { set; get; }

        public DoubleToStringConvert()
        {
            StringFormat = "0";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            if (value is double)
            {
                var v = (double)value;

                if (Math.Abs(v) < Double.Epsilon)
                {
                    return "0";
                }

                return (v).ToString(StringFormat);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value.ToString());
        }
    }
    public class SymbleConverter : IValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定源生成的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue || parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }
            var param = parameter.ToString().Split(' ');
            var tmpValue = (double)value;

            if (param.Length == 2)
            {
                return tmpValue.ToString(param[0]) + " " + param[1];
            }
            return tmpValue.ToString(param[0]);
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