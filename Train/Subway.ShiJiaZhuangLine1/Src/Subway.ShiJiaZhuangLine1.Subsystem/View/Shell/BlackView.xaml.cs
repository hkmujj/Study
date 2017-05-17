using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Shell
{
    /// <summary>
    /// BlackView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion, IsDefaultView = true)]
    public partial class BlackView : UserControl
    {
        public BlackView()
        {
            InitializeComponent();
        }
    }
}
