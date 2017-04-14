using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View
{
    /// <summary>
    /// ConfigureNavigateView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureNavigateRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ConfigureNavigateView
    {
        [ImportingConstructor]
        public ConfigureNavigateView(ConfigNavigateViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
