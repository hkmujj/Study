using System.Windows;
using System.Windows.Controls;

namespace Urban.ATC.Siemens.WPF.Control
{
    public class RecButton : Button
    {
        static RecButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecButton), new FrameworkPropertyMetadata(typeof(RecButton)));
        }
    }
}