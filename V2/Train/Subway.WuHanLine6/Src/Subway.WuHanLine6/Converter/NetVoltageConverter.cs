using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Subway.WuHanLine6.Converter
{
    /// <summary>
    ///
    /// </summary>
    public class NetVoltageConverter : IValueConverter
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
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (parameter != null)
            {
                if (parameter.ToString().Equals("Back"))
                {
                    Brush brush;

                    var tmpValue = (double)value;

                    if (tmpValue > 1000 && tmpValue < 1800)
                    {
                        brush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    }
                    else
                    {
                        brush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    }
                    return brush;
                }
                else
                {
                    Brush brush;

                    var tmpValue = (double)value;

                    if (tmpValue > 1000 && tmpValue < 1800)
                    {
                        brush = Brushes.Black;
                    }
                    else
                    {
                        brush = Brushes.White;
                    }
                    return brush;
                }
            }

            return DependencyProperty.UnsetValue;
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