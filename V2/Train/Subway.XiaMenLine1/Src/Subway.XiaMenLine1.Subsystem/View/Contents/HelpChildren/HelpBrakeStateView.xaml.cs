using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpBrakeStateView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 4)]
    public partial class HelpBrakeStateView : ITabItemInfoProvider
    {
        public HelpBrakeStateView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "制动状态";}}
    }
}
