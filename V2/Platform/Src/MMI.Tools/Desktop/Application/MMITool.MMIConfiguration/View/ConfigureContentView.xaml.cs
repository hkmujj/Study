using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View
{
    /// <summary>
    /// ConfigureView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureContentRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ConfigureContentView
    {
        [ImportingConstructor]
        public ConfigureContentView(ConfigureContentViewModel contentViewModel)
        {
            this.DataContext = contentViewModel;
            InitializeComponent();
        }
    }
}
