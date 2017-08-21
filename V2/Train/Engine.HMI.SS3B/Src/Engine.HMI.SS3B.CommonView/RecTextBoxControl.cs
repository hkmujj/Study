using System.Windows;
using System.Windows.Controls;

namespace Engine.HMI.SS3B.CommonView
{
    public class RecTextBoxControl : ContentControl
    {
        static RecTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecTextBoxControl), new FrameworkPropertyMetadata(typeof(RecTextBoxControl)));
        }
    }
}
