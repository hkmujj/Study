using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Engine._6A.CommonControl
{
    public class RecImageButton : Button
    {
        static RecImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecImageButton), new FrameworkPropertyMetadata(typeof(RecImageButton)));
        }

        public static readonly DependencyProperty ImageBrushProperty = DependencyProperty.Register(
            "ImageBrush", typeof(ImageBrush), typeof(RecImageButton), new PropertyMetadata(default(ImageBrush)));

        public ImageBrush ImageBrush
        {
            get { return (ImageBrush)GetValue(ImageBrushProperty); }
            set { SetValue(ImageBrushProperty, value); }
        }
    }
}