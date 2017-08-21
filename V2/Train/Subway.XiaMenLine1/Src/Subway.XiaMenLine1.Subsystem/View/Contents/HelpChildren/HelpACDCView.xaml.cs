using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
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
