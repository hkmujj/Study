using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmi.Common.Control.WPF
{
    /// <summary>
    /// 
    /// </summary>
    public class WrapListGroupHeader : ToggleButton
    {
        static WrapListGroupHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WrapListGroupHeader), new FrameworkPropertyMetadata(typeof(WrapListGroupHeader)));
        }
    }
}