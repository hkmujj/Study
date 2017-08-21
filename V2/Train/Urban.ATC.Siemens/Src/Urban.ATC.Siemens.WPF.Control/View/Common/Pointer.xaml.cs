using System.Windows;
using System.Windows.Controls;

namespace Urban.ATC.Siemens.WPF.Control.View.Common
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