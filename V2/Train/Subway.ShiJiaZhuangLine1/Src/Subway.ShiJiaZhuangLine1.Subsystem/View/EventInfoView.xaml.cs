using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View
{
    /// <summary>
    /// EventInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("事件信息")]
    public partial class EventInfoView
    {
        public EventInfoView()
        {
            InitializeComponent();
        }
    }
}