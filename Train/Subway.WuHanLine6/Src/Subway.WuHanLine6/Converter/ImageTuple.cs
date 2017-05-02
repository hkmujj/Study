using System;
using System.Windows;
using System.Windows.Media;

namespace Subway.WuHanLine6.Converter
{
    /// <summary>
    /// Image元组
    /// </summary>
    public class ImageTuple : DependencyObject
    {
        /// <summary>
        /// 图片源依赖属性
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(ImageSource), typeof(ImageTuple), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// 图片Source
        /// </summary>
        public ImageSource ImageSource { get { return (ImageSource)GetValue(ImageSourceProperty); } set { SetValue(ImageSourceProperty, value); } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="elment"></param>
        /// <param name="value"></param>
        public static void SetImageSource(DependencyObject elment, ImageSource value)
        {
            elment.SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="elment"></param>
        /// <returns></returns>
        public static ImageSource GetImageSource(DependencyObject elment)
        {
            return (ImageSource)elment.GetValue(ImageSourceProperty);
        }

        /// <summary>
        /// Key
        /// </summary>
        public Enum Key { get; set; }
    }
}