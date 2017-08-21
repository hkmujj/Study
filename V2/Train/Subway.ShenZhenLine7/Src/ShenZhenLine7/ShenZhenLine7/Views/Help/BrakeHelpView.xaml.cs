using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Views.Help
{
    /// <summary>
    /// BrakeHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 5)]
    public partial class BrakeHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public BrakeHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "制动状态"; } }
    }
}
