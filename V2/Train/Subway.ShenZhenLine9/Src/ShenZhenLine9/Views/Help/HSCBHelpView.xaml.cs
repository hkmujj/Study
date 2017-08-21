using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Views.Help
{
    /// <summary>
    /// HSCBHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 9)]
    public partial class HSCBHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public HSCBHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName { get { return "受电弓/HSCB"; } }
    }
}
