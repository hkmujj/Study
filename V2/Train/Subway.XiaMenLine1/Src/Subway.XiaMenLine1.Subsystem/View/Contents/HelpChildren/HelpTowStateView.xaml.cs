using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpTowStateView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 5)]
    public partial class HelpTowStateView : ITabItemInfoProvider
    {
        public HelpTowStateView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "牵引状态";}} 
    }
}
