using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Views.Help
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
