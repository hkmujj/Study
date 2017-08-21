using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Engine.LCDM.HXD3.Views.CommonView
{
    /// <summary>
    /// VerticalTextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class VerticalTextBlock : UserControl
    {
        public VerticalTextBlock()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(
            "TextStyle", typeof(Style), typeof(VerticalTextBlock), new PropertyMetadata(default(Style), OnTextStyleChanged));

        private static void OnTextStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tmp = d as VerticalTextBlock;
            if (tmp != null)
            {
                tmp.StyleChanged(e);
            }
        }

        private void StyleChanged(DependencyPropertyChangedEventArgs e)
        {
            var tmpStyle = e.NewValue as Style;
            if (tmpStyle != null)
            {
                TextBlock.Style = tmpStyle;
            }

        }
        public Style TextStyle
        {
            get { return (Style)GetValue(TextStyleProperty); }
            set { SetValue(TextStyleProperty, value); }
        }

        public static readonly DependencyProperty SplitCharProperty = DependencyProperty.Register(
            "SplitChar", typeof(string), typeof(VerticalTextBlock), new PropertyMetadata(default(string)));

        public string SplitChar
        {
            get { return (string)GetValue(SplitCharProperty); }
            set { SetValue(SplitCharProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(VerticalTextBlock), new PropertyMetadata(default(string), OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VerticalTextBlock)d).Changed(e);
        }
        private void Changed(DependencyPropertyChangedEventArgs e)
        {
            var tmpValue = e.NewValue as string;
            TextBlock.Inlines.Clear();
            if (tmpValue != null)
            {
                var first = true;

                //  tmpValue.Replace(SplitChar, "#").Split('#');
                foreach (var s in tmpValue.Replace(SplitChar, "#").Split('#'))
                {
                    if (!first)
                    {
                        TextBlock.Inlines.Add(new LineBreak());
                    }
                    TextBlock.Inlines.Add(s);
                    first = false;
                }
                //foreach (var tmp in tmpValue.Select(ch => ch.ToString()))
                //{
                //    if (!first)
                //    {
                //        TextBlock.Inlines.Add(new LineBreak());
                //    }
                //    TextBlock.Inlines.Add(tmp);
                //    first = false;
                //}
            }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
