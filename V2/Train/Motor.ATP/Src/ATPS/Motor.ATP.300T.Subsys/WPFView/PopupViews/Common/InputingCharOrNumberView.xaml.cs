using System.Windows;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Common
{
    /// <summary>
    /// InputingCharOrNumberView.xaml 的交互逻辑
    /// </summary>
    public partial class InputingCharOrNumberView 
    {
        public InputingCharOrNumberView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DriverInputStateProperty = DependencyProperty.Register(
            "DriverInputState", typeof (DriverInputState), typeof (InputingCharOrNumberView), new PropertyMetadata(default(DriverInputState)));

        public DriverInputState DriverInputState
        {
            get { return (DriverInputState) GetValue(DriverInputStateProperty); }
            set { SetValue(DriverInputStateProperty, value); }
        }
    }
}
