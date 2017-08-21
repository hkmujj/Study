using System.Windows;
using System.Windows.Controls;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// RectText.xaml 的交互逻辑
    /// </summary>
    public partial class RectText : UserControl
    {
        public RectText()
        {
            InitializeComponent();
        }
        public DependencyProperty ConrentProperty = DependencyProperty.Register("CurrentText", typeof(string), typeof(RectText), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,ContentChange));

        private static void ContentChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ((RectText)d).OnContentChanged(e);
        }

        private void OnContentChanged(DependencyPropertyChangedEventArgs e)
        {
            TextBlock.Text = CurrentText;
        }
        public string CurrentText { get { return this.GetValue(ConrentProperty).ToString(); } set { this.SetValue(ConrentProperty, value); } }

        
    }
}
