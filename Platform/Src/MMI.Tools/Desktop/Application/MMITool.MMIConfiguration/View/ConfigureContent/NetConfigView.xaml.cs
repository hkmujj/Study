using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent
{
    /// <summary>
    /// NetConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ViewMappedConfigType(typeof(NetConfigView), typeof(NetConfig), false)]
    public partial class NetConfigView
    {
        [ImportingConstructor]
        public NetConfigView(NetConfigViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
