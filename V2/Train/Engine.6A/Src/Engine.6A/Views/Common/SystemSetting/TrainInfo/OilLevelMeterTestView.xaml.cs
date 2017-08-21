using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting.TrainInfo
{
    /// <summary>
    /// OilLevelMeterTestView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainInfo, Priority = 3)]
    public partial class OilLevelMeterTestView : IPageName
    {
        public OilLevelMeterTestView()
        {
            InitializeComponent();
        }

        public string PageName
        {
            get { return "油位仪试验"; }
        }
    }
}
