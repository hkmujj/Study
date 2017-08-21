using System.Windows;

namespace Engine.Angola.TCMS.View.Common
{
    /// <summary>
    /// BorderLedText.xaml 的交互逻辑
    /// </summary>
    public partial class BorderText 
    {
        public BorderText()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StackPancelStyleProperty = DependencyProperty.Register(
            "StackPancelStyle", typeof(Style), typeof(BorderText), new PropertyMetadata(default(Style)));

        public Style StackPancelStyle
        {
            get { return (Style) GetValue(StackPancelStyleProperty); }
            set { SetValue(StackPancelStyleProperty, value); }
        }

        public static readonly DependencyProperty LeftTextProperty = DependencyProperty.Register(
            "LeftText", typeof(string), typeof(BorderText), new PropertyMetadata(default(string)));

        public string LeftText
        {
            get { return (string) GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }

        public static readonly DependencyProperty LeftTextStyleProperty = DependencyProperty.Register(
            "LeftTextStyle", typeof(Style), typeof(BorderText), new PropertyMetadata(default(Style)));

        public Style LeftTextStyle
        {
            get { return (Style) GetValue(LeftTextStyleProperty); }
            set { SetValue(LeftTextStyleProperty, value); }
        }


        public static readonly DependencyProperty RightTextProperty = DependencyProperty.Register(
            "RightText", typeof(string), typeof(BorderText), new PropertyMetadata(default(string)));

        public string RightText
        {
            get { return (string) GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }

        public static readonly DependencyProperty RightTextStyleProperty = DependencyProperty.Register(
            "RightTextStyle", typeof(Style), typeof(BorderText), new PropertyMetadata(default(Style)));

        public Style RightTextStyle
        {
            get { return (Style) GetValue(RightTextStyleProperty); }
            set { SetValue(RightTextStyleProperty, value); }
        }

        public static readonly DependencyProperty InnerBorderStyleProperty = DependencyProperty.Register(
            "InnerBorderStyle", typeof(Style), typeof(BorderText), new PropertyMetadata(default(Style)));

        public Style InnerBorderStyle
        {
            get { return (Style) GetValue(InnerBorderStyleProperty); }
            set { SetValue(InnerBorderStyleProperty, value); }
        }
    }
}
