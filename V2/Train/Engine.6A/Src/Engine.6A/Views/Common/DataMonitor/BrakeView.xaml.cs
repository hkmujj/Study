using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// BrakeView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.DataMonitorTabRegion, "true", "0", RegionNames.Axle8DataMonitorTabRegion, "false", "0" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BrakeView : ITabItemInfoProvider
    {
        public BrakeView()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "制 动"; } }
    }
}
