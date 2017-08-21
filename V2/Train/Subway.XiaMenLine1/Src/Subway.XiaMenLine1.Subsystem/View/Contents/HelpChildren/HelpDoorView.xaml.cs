using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpDoorView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 2)]
    public partial class HelpDoorView : ITabItemInfoProvider
    {
        public HelpDoorView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "门状态";}} 
    }
}
