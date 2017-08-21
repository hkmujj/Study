using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Views.Help
{
    /// <summary>
    /// AssistHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 2)]
    public partial class AssistHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public AssistHelpView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "辅助电源"; } }
    }
}
