using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.DataType.Config.Net.DataProtocol;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.Constant;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.ViewItem
{
    /// <summary>
    /// NetDataConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ViewMappedConfigType(typeof(NetDataConfigView), typeof(NetDataConfig), true)]
    public partial class NetDataConfigView
    {
        public NetDataConfigView()
        {
            InitializeComponent();
        }
    }
}
