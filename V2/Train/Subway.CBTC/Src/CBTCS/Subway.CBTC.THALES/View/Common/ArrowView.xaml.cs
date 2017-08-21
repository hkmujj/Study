using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Subway.CBTC.THALES.View.Common
{
    /// <summary>
    /// ArrowView.xaml 的交互逻辑
    /// </summary>
    public partial class ArrowView : UserControl
    {
        public ArrowView()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ContentBrushProperty =
            DependencyProperty.Register("ContentBrush",
                typeof(Brush),
                typeof(ArrowView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ContentBrushChanged,
                    null));

        private static void ContentBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ArrowView)d).OnContentBrushChanged(e);
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
                typeof(ArrowView),
                new FrameworkPropertyMetadata(0d,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PointerAngleChanged,
                    null));

        private static void PointerAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ArrowView)d).OnPointerAngleChanged(e);
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
