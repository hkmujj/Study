using System.Windows;
using System.Windows.Media;

namespace Urban.ATC.Siemens.WPF.Control.View.Common
{
    /// <summary>
    /// PointerView.xaml 的交互逻辑
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
            DependencyProperty.Register("CurrentSpeed",
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
            //PointerAngleBlock.CurrentSpeed = CurrentSpeed;
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

        //public static readonly DependencyProperty TextDataTemplateProperty =
        //    DependencyProperty.Register("TextDataTemplate",
        //        typeof(DataTemplate),
        //        typeof(PointerView),
        //        new FrameworkPropertyMetadata(null,
        //            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        //            TextDataTemplateChanged,
        //            null));

        //private static void TextDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((PointerView)d).OnTextDataTemplateChanged(e);
        //}

        //private void OnTextDataTemplateChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    //TextDataTemplateBlock.TextDataTemplate = TextDataTemplate;
        //}

        //public DataTemplate TextDataTemplate
        //{
        //    get
        //    {
        //        return (DataTemplate)this.GetValue(TextDataTemplateProperty);
        //    }
        //    set
        //    {
        //        this.SetValue(TextDataTemplateProperty, value);
        //    }
        //}

        //public static readonly DependencyProperty TextBrushProperty =
        //    DependencyProperty.Register("TextBrush",
        //        typeof(Brush),
        //        typeof(PointerView),
        //        new FrameworkPropertyMetadata(null,
        //            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        //            TextBrushChanged,
        //            null));

        //private static void TextBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((PointerView)d).OnTextBrushChanged(e);
        //}

        //private void OnTextBrushChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    TextBlock.Foreground = TextBrush;
        //}

        //public Brush TextBrush
        //{
        //    get
        //    {
        //        return (Brush)this.GetValue(TextBrushProperty);
        //    }
        //    set
        //    {
        //        this.SetValue(TextBrushProperty, value);
        //    }
        //}
    }
}