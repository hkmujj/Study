using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.Fault
{
    /// <summary>
    /// HistoryFauktContentPack.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.FaultTabRegion, "false", "1", RegionNames.Axle8FaultTabRegion, "false", "1" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class HistoryFauktContentPack : ITabItemInfoProvider
    {
        public HistoryFauktContentPack()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "历史记录"; }
        }
    }
}
