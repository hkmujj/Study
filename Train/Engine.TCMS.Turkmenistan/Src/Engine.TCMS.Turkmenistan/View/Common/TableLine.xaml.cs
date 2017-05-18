using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.TCMS.Turkmenistan.View.Common
{
    /// <summary>
    /// TableLine.xaml 的交互逻辑
    /// </summary>
    public partial class TableLine : UserControl
    {
        public TableLine()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register(
            "LineBrush", typeof(Brush), typeof(TableLine), new PropertyMetadata(default(Brush)));

        public Brush LineBrush { get { return (Brush)GetValue(LineBrushProperty); } set { SetValue(LineBrushProperty, value); } }

        public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.Register(
            "TextForeground", typeof(Brush), typeof(TableLine), new PropertyMetadata(default(Brush)));

        public Brush TextForeground { get { return (Brush)GetValue(TextForegroundProperty); } set { SetValue(TextForegroundProperty, value); } }

        public static readonly DependencyProperty TextOneProperty = DependencyProperty.Register(
            "TextOne", typeof(string), typeof(TableLine), new PropertyMetadata(default(string)));

        public string TextOne { get { return (string)GetValue(TextOneProperty); } set { SetValue(TextOneProperty, value); } }

        public static readonly DependencyProperty TextTwoProperty = DependencyProperty.Register(
            "TextTwo", typeof(string), typeof(TableLine), new PropertyMetadata(default(string)));

        public string TextTwo { get { return (string)GetValue(TextTwoProperty); } set { SetValue(TextTwoProperty, value); } }

        public static readonly DependencyProperty LineWidthProperty = DependencyProperty.Register(
            "LineWidth", typeof(double), typeof(TableLine), new PropertyMetadata(default(double)));

        public double LineWidth { get { return (double)GetValue(LineWidthProperty); } set { SetValue(LineWidthProperty, value); } }
    }
}
