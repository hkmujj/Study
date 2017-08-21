using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// MaintainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.LoginPopup.IsOpen = false;
            this.LoginPopup.IsOpen = true;
        }
    }
}
