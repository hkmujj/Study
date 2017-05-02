using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.WuHanLine6.Converter
{
    /// <summary>
    /// DataGrid行高转换
    /// </summary>
    public class DataGridHeightConverter : IValueConverter
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
            var param = parameter as DataGridHeightConverterArgs;
            if (value == null || value == DependencyProperty.UnsetValue || param == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return (((double)value) - param.ColunmHederHeight) / param.Rows;
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

    /// <summary>
    /// 行高转换参数
    /// </summary>
    public class DataGridHeightConverterArgs : DependencyObject
    {
        /// <summary>
        /// 行依赖属性
        /// </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows", typeof(int), typeof(DataGridHeightConverterArgs), new PropertyMetadata(default(int)));

        /// <summary>
        /// 行
        /// </summary>
        public int Rows { get { return (int)GetValue(RowsProperty); } set { SetValue(RowsProperty, value); } }

        /// <summary>
        ///
        /// </summary>
        public static readonly DependencyProperty ColunmHederHeightProperty = DependencyProperty.Register(
            "ColunmHederHeight", typeof(double), typeof(DataGridHeightConverterArgs), new PropertyMetadata(default(double)));

        /// <summary>
        /// 列标题高
        /// </summary>
        public double ColunmHederHeight { get { return (double)GetValue(ColunmHederHeightProperty); } set { SetValue(ColunmHederHeightProperty, value); } }
    }
}