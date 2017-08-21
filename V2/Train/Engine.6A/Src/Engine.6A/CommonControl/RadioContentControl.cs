using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Engine._6A.CommonControl
{
    public class RadioContentControl : RadioButton
    {
        static RadioContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioContentControl),
                new FrameworkPropertyMetadata(typeof(RadioContentControl)));
        }

        public RadioContentControl()
        {
            AddHandler(MouseLeftButtonDownEvent, new RoutedEventHandler((sender, args) => OnMouseLeftButtonDown((MouseButtonEventArgs)args)), true);
            AddHandler(MouseRightButtonDownEvent, new RoutedEventHandler((sender, args) => OnMouseRightButtonDown((MouseButtonEventArgs)args)), true);
            AddHandler(MouseLeftButtonUpEvent, new RoutedEventHandler((sender, args) => OnMouseLeftButtonUp((MouseButtonEventArgs)args)), true);
            //AddHandler(MouseRightButtonUpEvent, new RoutedEventHandler((sender, args) => OnMouseRightButtonUp((MouseButtonEventArgs)args)), true);
            //AddHandler(MouseDownEvent, new RoutedEventHandler((sender, args) => OnMouseDown((MouseButtonEventArgs)args)), true);
            //AddHandler(ClickEvent, new RoutedEventHandler((sender, args) => OnClick()), true);
            //AddHandler(MouseUpEvent, new RoutedEventHandler((sender, args) => OnMouseUp((MouseButtonEventArgs)args)), true);
            AddHandler(MouseMoveEvent, new RoutedEventHandler((sender, args) => OnMouseMove((MouseEventArgs)args)), true);
        }

        public static readonly DependencyProperty CheckedBorderBrushProperty = DependencyProperty.Register(
            "CheckedBorderBrush", typeof(Brush), typeof(RadioContentControl), new PropertyMetadata(default(Brush)));

        public Brush CheckedBorderBrush
        {
            get { return (Brush)GetValue(CheckedBorderBrushProperty); }
            set { SetValue(CheckedBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty CheckedBorderThicknessProperty = DependencyProperty.Register(
            "CheckedBorderThickness", typeof(Thickness), typeof(RadioContentControl), new PropertyMetadata(default(Thickness)));

        public Thickness CheckedBorderThickness
        {
            get { return (Thickness)GetValue(CheckedBorderThicknessProperty); }
            set { SetValue(CheckedBorderThicknessProperty, value); }
        }
    }
}