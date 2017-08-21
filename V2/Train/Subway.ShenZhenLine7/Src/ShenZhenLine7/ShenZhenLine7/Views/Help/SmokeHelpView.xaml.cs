using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Views.Help
{
    /// <summary>
    /// SmokeHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 7)]
    public partial class SmokeHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public SmokeHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "火烟状态"; } }
    }
}
