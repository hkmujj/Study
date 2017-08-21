using System.Windows;
using System.Windows.Controls;

namespace Other.ContactLine.JW4G.Views.CommonView
{
    /// <summary>
    /// DataSheet.xaml 的交互逻辑
    /// </summary>
    public partial class DataSheet : UserControl
    {
        public DataSheet()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextThreeContentProperty = DependencyProperty.Register(
            "TextThreeContent", typeof (string), typeof (DataSheet), new PropertyMetadata(default(string),OnTextThreeChanged));

        public string TextThreeContent
        {
            get { return (string) GetValue(TextThreeContentProperty); }
            set { SetValue(TextThreeContentProperty, value); }
        }

        public static readonly DependencyProperty TextTwoContentProperty = DependencyProperty.Register(
            "TextTwoContent", typeof (string), typeof (DataSheet), new PropertyMetadata(default(string),OnTextTwoChanged));

        public string TextTwoContent
        {
            get { return (string) GetValue(TextTwoContentProperty); }
            set { SetValue(TextTwoContentProperty, value); }
        }

        public static readonly DependencyProperty TextOneContextProperty = DependencyProperty.Register(
            "TextOneContext", typeof(string), typeof(DataSheet), new PropertyMetadata(default(string), OnTextOneChanged));
        public string TextOneContext
        {
            get { return (string)GetValue(TextOneContextProperty); }
            set { SetValue(TextOneContextProperty, value); }
        }

        private static void OnTextOneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = d as DataSheet;
            if (ui != null)
            {
                ui.TextOne.Text = e.NewValue.ToString();
            }
        }
        private static void OnTextTwoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = d as DataSheet;
            if (ui != null)
            {
                ui.TextTwo.Text = e.NewValue.ToString();
            }
        }
        private static void OnTextThreeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = d as DataSheet;
            if (ui != null)
            {
                ui.TextThree.Text = e.NewValue.ToString();
            }
        }
    }
}
