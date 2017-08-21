using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.Fault
{
    /// <summary>
    /// FaultViewContentPack.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.FaultTabRegion, "false", "0", RegionNames.Axle8FaultTabRegion, "false", "0" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class FaultViewContentPack : ITabItemInfoProvider
    {
        public FaultViewContentPack()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "本次上电"; }
        }
    }
}
