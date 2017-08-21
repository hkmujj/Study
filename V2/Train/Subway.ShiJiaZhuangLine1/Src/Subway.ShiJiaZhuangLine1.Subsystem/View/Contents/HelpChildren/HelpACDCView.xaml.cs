using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Interface;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpACDC.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, IsDefaultView = true, Priority = 0)]
    public partial class HelpACDCView : ITabItemInfoProvider
    {
        public HelpACDCView()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "空调";}} 
        
    }
}
