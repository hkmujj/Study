using System.Windows;

namespace Engine._6A.CommonControl
{
    /// <summary>
    /// ComboBox6A.xaml 的交互逻辑
    /// </summary>
    public partial class ComboBox6A
    {
        public ComboBox6A()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
           "Content", typeof(object), typeof(ComboBox6A), new PropertyMetadata(default(object)));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }


        private void PART_Popup_OnOpened(object sender, System.EventArgs e)
        {
            this.SelectedItem = null;

        }
    }
}
