using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Axle8
{
    /// <summary>
    /// Axle8RunLineOne.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Axle8DataMonitorTabRegion, Priority = 3)]
    public partial class Axle8RunLineOne : ITabItemInfoProvider
    {
        public Axle8RunLineOne()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "走 行 1"; } }
    }
}
