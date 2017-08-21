using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Engine.TPX21F.HXN5B.Converter
{
    public class SpeedValueToForegroundConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty WhiteBrushProperty = DependencyProperty.Register(
            "WhiteBrush", typeof(Brush), typeof(SpeedValueToForegroundConverter), new PropertyMetadata(default(Brush)));

        public Brush WhiteBrush
        {
            get { return (Brush) GetValue(WhiteBrushProperty); }
            set { SetValue(WhiteBrushProperty, value); }
        }

        public static readonly DependencyProperty RedBrushProperty = DependencyProperty.Register(
            "RedBrush", typeof(Brush), typeof(SpeedValueToForegroundConverter), new PropertyMetadata(default(Brush)));

        public Brush RedBrush
        {
            get { return (Brush) GetValue(RedBrushProperty); }
            set { SetValue(RedBrushProperty, value); }
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue && value is float)
            {
                if ((float)value >=System.Convert.ToSingle(parameter))
                {
                    return RedBrush;
                }
            }

            return WhiteBrush;
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}