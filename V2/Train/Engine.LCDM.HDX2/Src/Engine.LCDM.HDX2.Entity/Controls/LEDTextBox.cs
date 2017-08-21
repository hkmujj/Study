using System.Windows;
using System.Windows.Controls;

namespace Engine.LCDM.HDX2.Entity.Controls
{
    class LEDTextBox : TextBox
    {
        static LEDTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LEDTextBox), new FrameworkPropertyMetadata(typeof(LEDTextBox)));
        }
    }
}
