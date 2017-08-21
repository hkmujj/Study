using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Urban.GuiYang.DDU.View.Common
{
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageButton),
                new FrameworkPropertyMetadata(typeof (ImageButton)));
        }

        public static readonly DependencyProperty PressedImageProperty = DependencyProperty.Register(
            "PressedImage", typeof (ImageSource), typeof (ImageButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource PressedImage
        {
            get { return (ImageSource) GetValue(PressedImageProperty); }
            set { SetValue(PressedImageProperty, value); }
        }

        public static readonly DependencyProperty RelasedImageProperty = DependencyProperty.Register(
            "RelasedImage", typeof (ImageSource), typeof (ImageButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource RelasedImage
        {
            get { return (ImageSource) GetValue(RelasedImageProperty); }
            set { SetValue(RelasedImageProperty, value); }
        }

    }
}