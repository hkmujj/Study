using System.Windows;

namespace Engine.TPX21F.HXN5B.View.Contents.Brake.Detail
{
    /// <summary>
    /// PointrView.xaml 的交互逻辑
    /// </summary>
    public partial class PointerView 
    {
        public PointerView()
        {
            InitializeComponent();
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
            ( (PointerView)d ).OnPointerAngleChanged(e);
        }

        private void OnPointerAngleChanged(DependencyPropertyChangedEventArgs e)
        {
            RotateTransform.Angle = PointerAngle;
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
