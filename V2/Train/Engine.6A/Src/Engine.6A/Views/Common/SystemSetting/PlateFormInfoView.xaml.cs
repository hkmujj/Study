using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting
{
    /// <summary>
    /// PlateFormInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SystemTabTabRegion, Priority = 4)]
    public partial class PlateFormInfoView : ITabItemInfoProvider, IPageName
    {
        public PlateFormInfoView()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "平台信息"; }
        }

        public string PageName
        {
            get { return "6A中央处理平台版本信息"; }
        }
    }
}
