using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380D.Constant;

namespace Motor.HMI.CRH380D.View.Contents
{
    /// <summary>
    /// NullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.DomainShellContent, IsDefaultView = true)]
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }
    }
}
