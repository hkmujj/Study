using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.ShenZhenLine7.Converter
{
    /// <summary>
    /// 开始界面时间转换函数
    /// </summary>
    public class StartTimeConverter : IValueConverter
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
            var tmp = (DateTime)value;
            return $"{tmp.ToString("yyyy-MM-dd")} {tmp.ToString("hh:mm:ss")}    {GetDayOfWeek(tmp.DayOfWeek)}";
        }

        private string GetDayOfWeek(DayOfWeek day)
        {
            string reuslt;
            switch (day)
            {
                case DayOfWeek.Sunday:
                    reuslt = "星期日";
                    break;
                case DayOfWeek.Monday:
                    reuslt = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    reuslt = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    reuslt = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    reuslt = "星期四";
                    break;
                case DayOfWeek.Friday:
                    reuslt = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    reuslt = "星期六";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(day), day, null);
            }
            return reuslt;
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