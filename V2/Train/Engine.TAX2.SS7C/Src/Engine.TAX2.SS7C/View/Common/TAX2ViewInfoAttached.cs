using System.Windows;
using Engine.TAX2.SS7C.Model.Domain.Constant;

namespace Engine.TAX2.SS7C.View.Common
{
    public class TAX2ViewInfoAttached : DependencyObject
    {
        public static readonly DependencyProperty CommunicationStateProperty = DependencyProperty.RegisterAttached(
            "CommunicationState", typeof(TAX2CommunicationState), typeof(TAX2ViewInfoAttached),
            new PropertyMetadata(default(TAX2CommunicationState)));

        public static void SetCommunicationState(DependencyObject element, TAX2CommunicationState value)
        {
            element.SetValue(CommunicationStateProperty, value);
        }

        public static TAX2CommunicationState GetCommunicationState(DependencyObject element)
        {
            return (TAX2CommunicationState) element.GetValue(CommunicationStateProperty);
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached(
            "IsSelected", typeof(bool), typeof(TAX2ViewInfoAttached), new PropertyMetadata(default(bool)));

        public static void SetIsSelected(DependencyObject element, bool value)
        {
            element.SetValue(IsSelectedProperty, value);
        }

        public static bool GetIsSelected(DependencyObject element)
        {
            return (bool) element.GetValue(IsSelectedProperty);
        }
    }
}