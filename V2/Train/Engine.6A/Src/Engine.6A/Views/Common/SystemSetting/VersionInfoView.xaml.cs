using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting
{
    /// <summary>
    /// VersionInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SystemTabTabRegion, Priority = 3)]
    public partial class VersionInfoView : ITabItemInfoProvider, IPageName
    {
        public VersionInfoView()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "版本信息"; }
        }

        public string PageName
        {
            get { return "6A监测子系统的版本信息"; }
        }
    }
}
