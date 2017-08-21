using System.Windows;
using System.Windows.Media;

namespace Engine.TPX21F.HXN5B.View.Common
{
    /// <summary>
    /// PointView.xaml 的交互逻辑
    /// </summary>
    public partial class PointerView 
    {
        public PointerView()
        {
            InitializeComponent();
            
        }

        public static readonly DependencyProperty ContentBrushProperty =
            DependencyProperty.Register("ContentBrush",
                typeof(Brush),
                typeof(PointerView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ContentBrushChanged,
                    null));

        private static void ContentBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PointerView)d).OnContentBrushChanged(e);
        }

        public static readonly DependencyProperty PointerBrushProperty = DependencyProperty.Register(
            "PointerBrush", typeof(Brush), typeof(PointerView), new PropertyMetadata(default(Brush), PointerBurshPropertyChangedCallback));

        private static void PointerBurshPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ( (PointerView)d ).OnPointerBrushChanged(e);
        }

        public Brush PointerBrush
        {
            get { return (Brush) GetValue(PointerBrushProperty); }
            set { SetValue(PointerBrushProperty, value); }
        }

        public Brush ContentBrush
        {
            get
            {
                return (Brush)GetValue(ContentBrushProperty);
            }
            set
            {
                SetValue(ContentBrushProperty, value);
            }
        }

        public static readonly DependencyProperty PointerAngleProperty =
            DependencyProperty.Register("PointerAngle",
                typeof(double),
                typeof(PointerView),
                new FrameworkPropertyMetadata(0d,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PointerAngleChanged,
                    null));

        private static void PointerAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PointerView)d).OnPointerAngleChanged(e);
        }

        private void OnPointerAngleChanged(DependencyPropertyChangedEventArgs e)
        {
            RotateTransform.Angle = PointerAngle;
        }

        private void OnPointerBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            Rectangle1.Fill = PointerBrush;
            Rectangle2.Fill = PointerBrush;
            Rectangle3.Fill = PointerBrush;
            Rectangle4.Fill = PointerBrush;
        }

        private void OnContentBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            Ellipse.Fill = ContentBrush;

        }

        public double PointerAngle
        {
            get
            {
                return (double)GetValue(PointerAngleProperty);
            }
            set
            {
                SetValue(PointerAngleProperty, value);
            }
        }
    }
}
