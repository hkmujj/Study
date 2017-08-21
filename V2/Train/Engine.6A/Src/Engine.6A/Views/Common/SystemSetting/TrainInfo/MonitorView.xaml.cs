using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting.TrainInfo
{
    /// <summary>
    /// MonitorView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainInfo, Priority = 0)]
    public partial class MonitorView : IPageName
    {
        public MonitorView()
        {
            InitializeComponent();
        }

        public string PageName
        {
            get { return "监控信息"; }
        }
    }
}
