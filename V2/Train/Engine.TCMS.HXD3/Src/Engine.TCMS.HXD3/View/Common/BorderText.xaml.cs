using System.Windows;
using System.Windows.Media;

namespace Engine.TCMS.HXD3.View.Common
{
    /// <summary>
    /// BorderText.xaml 的交互逻辑
    /// </summary>
    public partial class BorderText
    {
        public BorderText()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(BorderText), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            "Foreground", typeof(Brush), typeof(BorderText), new PropertyMetadata(Brushes.White));

        public Brush Foreground
        {
            get { return (Brush) GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
    }
}