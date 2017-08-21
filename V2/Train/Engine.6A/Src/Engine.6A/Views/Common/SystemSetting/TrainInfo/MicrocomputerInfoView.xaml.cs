using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting.TrainInfo
{
    /// <summary>
    /// MicrocomputerInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainInfo, Priority = 1)]
    public partial class MicrocomputerInfoView : IPageName
    {
        public MicrocomputerInfoView()
        {
            InitializeComponent();
        }

        public string PageName
        {
            get { return "微机状态"; }
        }
    }
}
