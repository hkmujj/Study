using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting.TrainInfo
{
    /// <summary>
    /// ElectropsychometerTestView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainInfo, Priority = 2)]
    public partial class ElectropsychometerTestView : IPageName
    {
        public ElectropsychometerTestView()
        {
            InitializeComponent();
        }

        public string PageName
        {
            get { return "电能表试验"; }
        }
    }
}
