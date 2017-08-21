using System.Windows;
using System.Windows.Controls;

namespace Engine.HMI.SS3B.CommonView
{
    public class RadiusRectTextControl : ContentControl
    {
        static RadiusRectTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadiusRectTextControl), new FrameworkPropertyMetadata(typeof(RadiusRectTextControl)));
        }
    }
}