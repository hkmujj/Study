using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Interface;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelSmogView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 6)]
    public partial class HelpSmogView : ITabItemInfoProvider
    {
        public HelpSmogView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "烟温探测";}} 
    }
}
