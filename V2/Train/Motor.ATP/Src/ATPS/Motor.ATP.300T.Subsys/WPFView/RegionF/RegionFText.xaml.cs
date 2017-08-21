using System.Linq;
using System.Text;
using System.Windows;

namespace Motor.ATP._300T.Subsys.WPFView.RegionF
{
    /// <summary>
    /// RegionFText.xaml 的交互逻辑
    /// </summary>
    public partial class RegionFText
    {
        public RegionFText()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (RegionFText), new PropertyMetadata(default(string), TextPropertyChangedCallback));

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ob = (RegionFText) dependencyObject;
            ob.OnTextPropertyChangedCallback(dependencyPropertyChangedEventArgs);
        }

        private void OnTextPropertyChangedCallback(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var tb = Encoding.Default.GetBytes(Text);
            // 三个中文
            if (tb.Length > 6)
            {
                if (Text.Contains("RBC"))
                {
                    TextUp.Text = new string(Text.Take(3).ToArray());
                    TextDown.Text = new string(Text.Skip(3).ToArray());
                    TextOneLine.Text = string.Empty;
                }
                else
                {
                    TextUp.Text = new string(Text.Take(2).ToArray());
                    TextDown.Text = new string(Text.Skip(2).ToArray());
                    TextOneLine.Text = string.Empty;
                }
            }
            else
            {
                TextOneLine.Text = Text;
                TextUp.Text = string.Empty;
                TextDown.Text = string.Empty;
            }
        }



        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty OutlineVisibleProperty = DependencyProperty.Register(
    "OutlineVisible", typeof(bool), typeof(RegionFText), new PropertyMetadata(default(bool), OutlineVisiblePropertyChangedCallback));

        private static void OutlineVisiblePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ob = (RegionFText)dependencyObject;
            ob.OnOutlineVisiblePropertyChangedCallback(dependencyPropertyChangedEventArgs);
        }

        private void OnOutlineVisiblePropertyChangedCallback(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (OutlineVisible == true)
            {
                Outline.Visibility = Visibility.Visible;
            }
            else
            {
                Outline.Visibility = Visibility.Hidden;
            }
        }

        public bool OutlineVisible
        {
            get { return (bool)GetValue(OutlineVisibleProperty); ; }
            set
            {
                SetValue(OutlineVisibleProperty, value);
            }
        }
    }
}
