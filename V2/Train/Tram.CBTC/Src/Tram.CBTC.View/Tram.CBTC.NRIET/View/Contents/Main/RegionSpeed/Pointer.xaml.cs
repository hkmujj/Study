using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionSpeed
{
    /// <summary>
    /// Pointer.xaml 的交互逻辑
    /// </summary>
    public partial class Pointer : UserControl
    {
        public Pointer()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentBrushProperty =
            DependencyProperty.Register("ContentBrush",
                typeof(System.Windows.Media.Brush),
                typeof(Pointer),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ContentBrushChanged,
                    null));

        private static void ContentBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).OnContentBrushChanged(e);
        }

        private void OnContentBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            Ellipse.Stroke = ContentBrush;
            Rectangle1.Fill = ContentBrush;
            if (!PointerVisibility)
            {
                Ellipse.Fill = ContentBrush;
            }
        }

        /// <summary>
        /// 指针填充色
        /// </summary>
        public System.Windows.Media.Brush ContentBrush
        {
            get { return (System.Windows.Media.Brush)this.GetValue(ContentBrushProperty); }
            set { this.SetValue(ContentBrushProperty, value); }
        }

        public static readonly DependencyProperty PointerAngleProperty =
            DependencyProperty.Register("PointerAngle",
                typeof(double),
                typeof(Pointer),
                new FrameworkPropertyMetadata(0d,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PointerAngleChanged,
                    null));

        private static void PointerAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).OnPointerAngleChanged(e);
        }

        private void OnPointerAngleChanged(DependencyPropertyChangedEventArgs e)
        {
            //PointerAngleBlock.PointerAngle = PointerAngle;
            this.RotateTransform.Angle = PointerAngle;
        }
        
        /// <summary>
        /// 指针角度
        /// </summary>
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

        public static readonly DependencyProperty SpeedValueProperty = DependencyProperty.Register(
            "SpeedValue", typeof(double), typeof(Pointer), new PropertyMetadata(default(double), OnSpeedValueChanged));

        private static void OnSpeedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).SpeedValueChanged();
        }

        private void SpeedValueChanged()
        {
            //Speed.Text = SpeedValue.ToString("F0");
        }

        /// <summary>
        /// 速度值
        /// </summary>
        public double SpeedValue { get { return (double)GetValue(SpeedValueProperty); } set { SetValue(SpeedValueProperty, value); } }

        public static readonly DependencyProperty SpeedBrushProperty = DependencyProperty.Register(
            "SpeedBrush", typeof(System.Windows.Media.Brush), typeof(Pointer), new PropertyMetadata(default(System.Windows.Media.Brush), OnSpeedBrushChanged));

        private static void OnSpeedBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).SpeedBrushChanged();
        }

        private void SpeedBrushChanged()
        {
            Rectangle1.Fill = SpeedBrush;
        }

        /// <summary>
        /// 速度颜色
        /// </summary>
        public System.Windows.Media.Brush SpeedBrush { get { return (System.Windows.Media.Brush)GetValue(SpeedBrushProperty); } set { SetValue(SpeedBrushProperty, value); } }


        public static readonly DependencyProperty PointerVisibilityProperty = DependencyProperty.Register(
            "PointerVisibility", typeof(bool), typeof(Pointer), new PropertyMetadata(default(bool), OnPointerVisibilityChanged));

        private static void OnPointerVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).PointerVisibilityChanged();
        }

        private void PointerVisibilityChanged()
        {
            if (PointerVisibility)
            {
                Ellipse.Fill = new SolidColorBrush(Color.FromRgb(255,255,255));
                Ellipse.Stroke = ContentBrush;
                Ellipse.Visibility = Visibility.Visible;
                Rectangle1.Visibility = Visibility.Visible;
                //Rectangle2.Visibility = Visibility.Visible;
                //Rectangle3.Visibility = Visibility.Visible;
                //Rectangle4.Visibility = Visibility.Visible;
                //Speed.Visibility = Visibility.Visible;
            }
            else
            {
                Rectangle1.Visibility = Visibility.Hidden;
                //Rectangle2.Visibility = Visibility.Hidden;
                //Rectangle3.Visibility = Visibility.Hidden;
                //Rectangle4.Visibility = Visibility.Hidden;
                //Speed.Visibility = Visibility.Hidden;
                Ellipse.Visibility = Visibility.Visible;
                Ellipse.Stroke = ContentBrush;
                Ellipse.Fill = ContentBrush;
            }
        }

        /// <summary>
        /// 速度指针显示设定
        /// </summary>
        public bool PointerVisibility { get { return (bool)GetValue(PointerVisibilityProperty); } set { SetValue(PointerVisibilityProperty, value); } }

    }
}