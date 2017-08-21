using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Views.Help
{
    /// <summary>
    /// AirPumpHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 8)]
    public partial class AirPumpHelpView : ITabItemInfoProvider
    {
        public AirPumpHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "空气压缩机"; } }
    }
}
