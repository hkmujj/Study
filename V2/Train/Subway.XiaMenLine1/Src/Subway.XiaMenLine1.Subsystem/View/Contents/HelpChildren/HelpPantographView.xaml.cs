using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpPantographView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 8)]
    public partial class HelpPantographView : ITabItemInfoProvider
    {
        public HelpPantographView()
        {
            InitializeComponent();
        }
        public string HeadName { get { return "受电弓/HSCB";}} 
    }
}
