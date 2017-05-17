using System.Windows;
using System.Windows.Controls;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Controls
{
    public class RectangleButton : Button
    {
        static RectangleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RectangleButton), new FrameworkPropertyMetadata(typeof(RectangleButton)));
        }
    }
}