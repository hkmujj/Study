using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// FirePreventionView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.DataMonitorTabRegion, "false", "1", RegionNames.Axle8DataMonitorTabRegion, "false", "1" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class FirePreventionView : ITabItemInfoProvider
    {
        public FirePreventionView()
        {
            InitializeComponent();
            
        }

        public string HeadName { get { return "防 火"; } }


      
    }
}
