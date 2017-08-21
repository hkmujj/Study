using System.Windows;
using System.Windows.Controls;

namespace Engine.HMI.SS3B.CommonView
{
    public class RecTextButton : Button
    {
        static RecTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecTextButton), new FrameworkPropertyMetadata(typeof(RecTextButton)));
        }
    }
}