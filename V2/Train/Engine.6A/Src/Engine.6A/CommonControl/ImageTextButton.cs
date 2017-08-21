using System.Windows;
using System.Windows.Controls;

namespace Engine._6A.CommonControl
{
    public class ImageTextButton : Button
    {
        static ImageTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageTextButton), new FrameworkPropertyMetadata(typeof(ImageTextButton)));
        }
    }
}