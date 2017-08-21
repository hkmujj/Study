using System.Windows;
using System.Windows.Controls;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Controls
{
    public class BorderText : TextBox
    {
        static BorderText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderText), new FrameworkPropertyMetadata(typeof(BorderText)));
        }
    }
}