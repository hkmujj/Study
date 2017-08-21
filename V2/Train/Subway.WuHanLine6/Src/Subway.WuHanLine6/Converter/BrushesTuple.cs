using System;
using System.Windows;
using System.Windows.Media;

namespace Subway.WuHanLine6.Converter
{
    /// <summary>
    /// 画刷元组
    /// </summary>
    public class BrushesTuple : DependencyObject
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
            "Brush", typeof(Brush), typeof(BrushesTuple), new PropertyMetadata(default(Brush)));

        /// <summary>
        ///
        /// </summary>
        public Brush Brush { get { return (Brush)GetValue(BrushProperty); } set { SetValue(BrushProperty, value); } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="elmnet"></param>
        /// <param name="value"></param>
        public static void SetBrush(DependencyObject elmnet, Brush value)
        {
            elmnet.SetValue(BrushProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(BrushProperty);
        }

        /// <summary>
        /// Key
        /// </summary>
        public Enum Key { get; set; }
    }
}