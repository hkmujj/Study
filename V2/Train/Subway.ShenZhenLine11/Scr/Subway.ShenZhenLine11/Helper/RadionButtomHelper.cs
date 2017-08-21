using System.Windows;
using System.Windows.Media;

namespace Subway.ShenZhenLine11.Helper
{
    public class RadionButtomHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CheckedImageProperty = DependencyProperty.RegisterAttached(
            "CheckedImage", typeof(ImageSource), typeof(RadionButtomHelper), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCheckedImage(DependencyObject element, ImageSource value)
        {
            element.SetValue(CheckedImageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ImageSource GetCheckedImage(DependencyObject element)
        {
            return (ImageSource)element.GetValue(CheckedImageProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty NormalImageProperty = DependencyProperty.RegisterAttached(
            "NormalImage", typeof(ImageSource), typeof(RadionButtomHelper), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetNormalImage(DependencyObject element, ImageSource value)
        {
            element.SetValue(NormalImageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ImageSource GetNormalImage(DependencyObject element)
        {
            return (ImageSource)element.GetValue(NormalImageProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty FaultImageProperty = DependencyProperty.RegisterAttached(
            "FaultImage", typeof(ImageSource), typeof(RadionButtomHelper), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFaultImage(DependencyObject element, ImageSource value)
        {
            element.SetValue(FaultImageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ImageSource GetFaultImage(DependencyObject element)
        {
            return (ImageSource)element.GetValue(FaultImageProperty);
        }
    }
}