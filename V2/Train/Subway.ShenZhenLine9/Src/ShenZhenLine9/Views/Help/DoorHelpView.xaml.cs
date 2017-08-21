using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Views.Help
{
    /// <summary>
    ///     DoorHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 3)]
    public partial class DoorHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// </summary>
        public DoorHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     标题名称
        /// </summary>
        public string HeadName { get { return "门状态"; } }
    }
}
