using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Interface;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpEmgCommunicationView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 3)]
    public partial class HelpEmgCommunicationView : ITabItemInfoProvider
    {
        public HelpEmgCommunicationView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "紧急通讯";}}
    }
}
