using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Engine.TPX21F.HXN5B.Converter
{
    public class TowBrakeNewtToForegroundConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TowBrushProperty = DependencyProperty.Register(
            "TowBrush", typeof(Brush), typeof(TowBrakeNewtToForegroundConverter), new PropertyMetadata(default(Brush)));

        public Brush TowBrush
        {
            get { return (Brush) GetValue(TowBrushProperty); }
            set { SetValue(TowBrushProperty, value); }
        }

        public static readonly DependencyProperty BrakeBrushProperty = DependencyProperty.Register(
            "BrakeBrush", typeof(Brush), typeof(TowBrakeNewtToForegroundConverter), new PropertyMetadata(default(Brush)));

        public Brush BrakeBrush
        {
            get { return (Brush) GetValue(BrakeBrushProperty); }
            set { SetValue(BrakeBrushProperty, value); }
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue && value is double)
            {
                return (double)value > 0 ? TowBrush : BrakeBrush;
            }

            return DependencyProperty.UnsetValue;
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