using System.Windows;
using System.Windows.Controls;

namespace Engine._6A.CommonControl
{
    public class RecTextButton : Button
    {
        static RecTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecTextButton), new FrameworkPropertyMetadata(typeof(RecTextButton)));
        }
    }
}