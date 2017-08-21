using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Views.Help
{
    /// <summary>
    /// EmergencyTalkHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 4)]
    public partial class EmergencyTalkHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public EmergencyTalkHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "紧急对讲"; } }
    }
}
