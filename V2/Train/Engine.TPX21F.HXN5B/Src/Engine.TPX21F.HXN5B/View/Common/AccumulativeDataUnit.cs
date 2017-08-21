using System.Windows;

namespace Engine.TPX21F.HXN5B.View.Common
{
    public class AccumulativeDataUnit : DependencyObject
    {
        public static readonly DependencyProperty UnitProperty = DependencyProperty.RegisterAttached(
            "Unit", typeof(string), typeof(AccumulativeDataUnit), new PropertyMetadata(default(string)));

        public static void SetUnit(DependencyObject element, string value)
        {
            element.SetValue(UnitProperty, value);
        }

        public static string GetUnit(DependencyObject element)
        {
            return (string) element.GetValue(UnitProperty);
        }
    }
}