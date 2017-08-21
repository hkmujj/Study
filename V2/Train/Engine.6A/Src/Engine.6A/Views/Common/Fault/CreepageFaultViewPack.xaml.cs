using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.Fault
{
    /// <summary>
    /// CreepageFaultViewPack.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.FaultTabRegion, Priority = 2)]
    public partial class CreepageFaultViewPack : ITabItemInfoProvider
    {
        public CreepageFaultViewPack()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "漏电记录"; }
        }
    }
}
