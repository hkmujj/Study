using System.Windows;

namespace Motor.ATP._200H.Subsys.WPFView.RegionF
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
            "Text", typeof(string), typeof(RegionFText), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
