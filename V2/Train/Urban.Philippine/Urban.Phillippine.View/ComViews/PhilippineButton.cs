using System.Windows;
using System.Windows.Controls;

namespace Urban.Phillippine.View.ComViews
{
    public class PhilippineButton : Button
    {
        static PhilippineButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhilippineButton),
                new FrameworkPropertyMetadata(typeof(PhilippineButton)));
        }
    }
}