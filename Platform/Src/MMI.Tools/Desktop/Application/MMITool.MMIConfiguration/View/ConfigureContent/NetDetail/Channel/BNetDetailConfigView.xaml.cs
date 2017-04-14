using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel
{
    /// <summary>
    /// BNetDetailConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.NetDetailConfigRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class BNetDetailConfigView
    {
        [ImportingConstructor]
        public BNetDetailConfigView(NetConfigViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
