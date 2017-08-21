using System.Windows;
using System.Windows.Controls;

namespace Subway.XiaMenLine1.Subsystem.Controls
{
    public class RecTextButton : Button
    {
        static RecTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecTextButton), new FrameworkPropertyMetadata(typeof(RecTextButton)));
        }
    }
}
