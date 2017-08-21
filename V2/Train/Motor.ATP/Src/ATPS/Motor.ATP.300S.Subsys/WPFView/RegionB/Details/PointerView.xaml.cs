using System.Windows;
using System.Windows.Media;

namespace Motor.ATP._300S.Subsys.WPFView.RegionB.Details
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

        private void OnContentBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            Ellipse.Fill = ContentBrush;
            Rectangle1.Fill = ContentBrush;
            Rectangle2.Fill = ContentBrush;
            Rectangle3.Fill = ContentBrush;
            Rectangle4.Fill = ContentBrush;
        }

        public Brush ContentBrush
        {
            get
            {
                return (Brush)this.GetValue(ContentBrushProperty);
            }
            set
            {
                this.SetValue(ContentBrushProperty, value);
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
            this.RotateTransform.Angle = PointerAngle;
        }

        public double PointerAngle
        {
            get
            {
                return (double)this.GetValue(PointerAngleProperty);
            }
            set
            {
                this.SetValue(PointerAngleProperty, value);
            }
        }
    }
}
