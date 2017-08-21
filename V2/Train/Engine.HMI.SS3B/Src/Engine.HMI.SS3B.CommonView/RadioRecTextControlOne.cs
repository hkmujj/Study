using System.Windows;
using System.Windows.Controls;

namespace Engine.HMI.SS3B.CommonView
{
    public class RadioRecTextControlOne : ContentControl
    {
        static RadioRecTextControlOne()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioRecTextControlOne), new FrameworkPropertyMetadata(typeof(RadioRecTextControlOne)));
        }
    }
}
