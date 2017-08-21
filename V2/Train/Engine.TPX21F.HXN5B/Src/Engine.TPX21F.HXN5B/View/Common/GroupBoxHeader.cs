using System.Windows;

namespace Engine.TPX21F.HXN5B.View.Common
{
    public class GroupBoxHeader : DependencyObject
    {
        public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.RegisterAttached(
            "HeaderMargin", typeof(double), typeof(GroupBoxHeader), new PropertyMetadata(default(double)));

        public static void SetHeaderMargin(DependencyObject element, double value)
        {
            element.SetValue(HeaderMarginProperty, value);
        }

        public static double GetHeaderMargin(DependencyObject element)
        {
            return (double) element.GetValue(HeaderMarginProperty);
        }

        public static readonly DependencyProperty HeaderWidthProperty = DependencyProperty.RegisterAttached(
            "HeaderWidth", typeof(double), typeof(GroupBoxHeader), new PropertyMetadata(default(double)));

        public static void SetHeaderWidth(DependencyObject element, double value)
        {
            element.SetValue(HeaderWidthProperty, value);
        }

        public static double GetHeaderWidth(DependencyObject element)
        {
            return (double) element.GetValue(HeaderWidthProperty);
        }
    }
}