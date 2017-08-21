using System.Windows;
using System.Windows.Controls;

namespace Subway.WuHanLine6.GIS.Views.CommonView
{
    /// <summary>
    /// ChineseEnglishText.xaml 的交互逻辑
    /// </summary>
    public partial class ChineseEnglishText : UserControl
    {
        public ChineseEnglishText()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ChineseTextProperty = DependencyProperty.Register(
            "ChineseText", typeof(string), typeof(ChineseEnglishText), new PropertyMetadata(default(string)));

        public string ChineseText { get { return (string)GetValue(ChineseTextProperty); } set { SetValue(ChineseTextProperty, value); } }

        public static readonly DependencyProperty EnglishTextProperty = DependencyProperty.Register(
            "EnglishText", typeof(string), typeof(ChineseEnglishText), new PropertyMetadata(default(string)));

        public string EnglishText { get { return (string)GetValue(EnglishTextProperty); } set { SetValue(EnglishTextProperty, value); } }

        public static readonly DependencyProperty ChineseTextStyleProperty = DependencyProperty.Register(
            "ChineseTextStyle", typeof(Style), typeof(ChineseEnglishText), new PropertyMetadata(default(Style)));

        public Style ChineseTextStyle { get { return (Style)GetValue(ChineseTextStyleProperty); } set { SetValue(ChineseTextStyleProperty, value); } }

        public static readonly DependencyProperty EnglishTextStyleProperty = DependencyProperty.Register(
            "EnglishTextStyle", typeof(Style), typeof(ChineseEnglishText), new PropertyMetadata(default(Style)));

        public Style EnglishTextStyle { get { return (Style)GetValue(EnglishTextStyleProperty); } set { SetValue(EnglishTextStyleProperty, value); } }
    }
}
