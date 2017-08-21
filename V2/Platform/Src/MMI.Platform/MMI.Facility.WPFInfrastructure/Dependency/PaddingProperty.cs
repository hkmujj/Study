using System.Windows;
using System.Windows.Controls;

namespace MMI.Facility.WPFInfrastructure.Dependency
{
    /// <summary>
    /// 提供 Padding 的绑定
    /// </summary>
    public class PaddingProperty
    {
        /// <summary>
        /// Padding.Left
        /// </summary>
        public static readonly DependencyProperty LeftProperty = DependencyProperty
            .RegisterAttached(
                "Left", typeof(double), typeof(PaddingProperty),
                new PropertyMetadata(default(double), LeftPropertyChangedCallback));

        /// <summary>
        /// SetLeft
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetLeft(DependencyObject element, double value)
        {
            element.SetValue(LeftProperty, value);
        }

        /// <summary>
        /// GetLeft
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetLeft(DependencyObject element)
        {
            return (double)element.GetValue(LeftProperty);
        }


        private static void LeftPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {

            var fe = d as Control;
            if (fe == null)
            {
                return;
            }

            fe.Padding = new Thickness((double)args.NewValue, fe.Padding.Top, fe.Padding.Right, fe.Padding.Bottom);
        }

        /// <summary>
        /// Padding.Right
        /// </summary>
        public static readonly DependencyProperty RightProperty = DependencyProperty
            .RegisterAttached(
                "Right", typeof(double), typeof(PaddingProperty),
                new PropertyMetadata(default(double), RightPropertyChangedCallback));

        /// <summary>
        /// SetRight
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetRight(DependencyObject element, double value)
        {
            element.SetValue(RightProperty, value);
        }

        /// <summary>
        /// GetRight
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetRight(DependencyObject element)
        {
            return (double)element.GetValue(RightProperty);
        }


        private static void RightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var fe = d as Control;
            if (fe == null)
            {
                return;
            }

            fe.Padding = new Thickness(fe.Padding.Left, fe.Padding.Top, (double)args.NewValue, fe.Padding.Bottom);
        }

        /// <summary>
        /// Padding.Top
        /// </summary>
        public static readonly DependencyProperty TopProperty = DependencyProperty
            .RegisterAttached(
                "Top", typeof(double), typeof(PaddingProperty),
                new PropertyMetadata(default(double), TopPropertyChangedCallback));

        /// <summary>
        /// SetTop
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTop(DependencyObject element, double value)
        {
            element.SetValue(TopProperty, value);
        }

        /// <summary>
        /// GetTop
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetTop(DependencyObject element)
        {
            return (double)element.GetValue(TopProperty);
        }


        private static void TopPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var fe = d as Control;
            if (fe == null)
            {
                return;
            }

            fe.Padding = new Thickness(fe.Padding.Left, (double)args.NewValue, fe.Padding.Right, fe.Padding.Bottom);
        }

        /// <summary>
        /// Padding.Bottom
        /// </summary>
        public static readonly DependencyProperty BottomProperty = DependencyProperty
            .RegisterAttached(
                "Bottom", typeof(double), typeof(PaddingProperty),
                new PropertyMetadata(default(double), BottomPropertyChangedCallback));

        /// <summary>
        /// SetBottom
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetBottom(DependencyObject element, double value)
        {
            element.SetValue(BottomProperty, value);
        }

        /// <summary>
        /// GetBottom
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetBottom(DependencyObject element)
        {
            return (double)element.GetValue(BottomProperty);
        }


        private static void BottomPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var fe = d as Control;
            if (fe == null)
            {
                return;
            }

            fe.Padding = new Thickness(fe.Padding.Left, fe.Padding.Top, fe.Padding.Right, (double)args.NewValue);
        }
    }
}