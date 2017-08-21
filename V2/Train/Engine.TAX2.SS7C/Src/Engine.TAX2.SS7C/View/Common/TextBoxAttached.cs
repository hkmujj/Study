using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.View.Common
{
    public class TextBoxAttached : NotificationObject
    {
        public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.RegisterAttached(
            "SelectionStart", typeof(int), typeof(TextBoxAttached), new PropertyMetadata(default(int), SelectionStartPropertyChangedCallback));

        private static void SelectionStartPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var txtBox = d as TextBox;
            if (txtBox != null)
            {
                txtBox.CaretIndex = GetSelectionStart(txtBox);
                //txtBox.Select(GetSelectionStart(txtBox), 1);
            }
        }

        public static void SetSelectionStart(DependencyObject element, int value)
        {
            element.SetValue(SelectionStartProperty, value);
        }

        public static int GetSelectionStart(DependencyObject element)
        {
            return (int) element.GetValue(SelectionStartProperty);
        }
    }
}