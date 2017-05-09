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

namespace Engine.TCMS.Turkmenistan.View.Common
{
    /// <summary>
    /// EffectBorder.xaml 的交互逻辑
    /// </summary>
    public partial class EffectBorder : UserControl
    {
        public EffectBorder()
        {
            InitializeComponent();
            Loaded += EffectBorder_Loaded;
        }

        private void EffectBorder_Loaded(object sender, RoutedEventArgs e)
        {
            WithChanged();
        }

        public static readonly DependencyProperty LineWidthProperty = DependencyProperty.Register(
            "LineWidth", typeof(double), typeof(EffectBorder), new PropertyMetadata(2d, OnWidthChanged));

        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EffectBorder)(d)).WithChanged();
        }

        private void WithChanged()
        {
            var width = ActualWidth;
            var height = ActualHeight;


            Date1 = $"M 0,0 L 0,{height} {LineWidth},{height - LineWidth} {LineWidth},{LineWidth} {width- LineWidth},{LineWidth} {width},0";
            Date2 = $"M 0,{height} L {width},{height} {width},0 {width - LineWidth},{LineWidth} {width - LineWidth},{height - LineWidth} {LineWidth},{height - LineWidth}";


        }

        public static readonly DependencyProperty EffectContentProperty = DependencyProperty.Register(
            "EffectContent", typeof(UIElement), typeof(EffectBorder), new PropertyMetadata(default(UIElement)));

        public UIElement EffectContent { get { return (UIElement)GetValue(EffectContentProperty); } set { SetValue(EffectContentProperty, value); } }

        public static readonly DependencyProperty Date1Property = DependencyProperty.Register(
            "Date1", typeof(string), typeof(EffectBorder), new PropertyMetadata(default(string)));

        public string Date1 { get { return (string)GetValue(Date1Property); } set { SetValue(Date1Property, value); } }

        public static readonly DependencyProperty Date2Property = DependencyProperty.Register(
            "Date2", typeof(string), typeof(EffectBorder), new PropertyMetadata(default(string)));

        public string Date2 { get { return (string)GetValue(Date2Property); } set { SetValue(Date2Property, value); } }
        public double LineWidth { get { return (double)GetValue(LineWidthProperty); } set { SetValue(LineWidthProperty, value); } }

        public static readonly DependencyProperty Line1FillProperty = DependencyProperty.Register(
            "Line1Fill", typeof(Brush), typeof(EffectBorder), new PropertyMetadata(default(Brush)));

        public Brush Line1Fill { get { return (Brush)GetValue(Line1FillProperty); } set { SetValue(Line1FillProperty, value); } }

        public static readonly DependencyProperty Line2FillProperty = DependencyProperty.Register(
            "Line2Fill", typeof(Brush), typeof(EffectBorder), new PropertyMetadata(default(Brush)));

        public Brush Line2Fill { get { return (Brush)GetValue(Line2FillProperty); } set { SetValue(Line2FillProperty, value); } }

      
    }
}
