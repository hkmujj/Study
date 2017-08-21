using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionSpeed
{
    /// <summary>
    /// EmergencyAngle.xaml 的交互逻辑
    /// </summary>
    public partial class Emergency : UserControl
    {
        public Emergency()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentBrushProperty =
         DependencyProperty.Register("ContentBrush",
             typeof(Brush),
             typeof(Emergency),
             new FrameworkPropertyMetadata(null,
                 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                 ContentBrushChanged,
                 null));

        private static void ContentBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Emergency)d).OnContentBrushChanged(e);
        }

        private void OnContentBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Polygon.Fill = ContentBrush;
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
               typeof(Emergency),
               new FrameworkPropertyMetadata(0d,
                   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                   PointerAngleChanged,
                   null));

        private static void PointerAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Emergency)d).OnPointerAngleChanged(e);
        }

        private void OnPointerAngleChanged(DependencyPropertyChangedEventArgs e)
        {
            //PointerAngleBlock.PointerAngle = PointerAngle;
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