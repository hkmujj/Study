using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent
{
    /// <summary>
    /// DebugWindowConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ViewMappedConfigType(typeof(DebugWindowConfigView), typeof(DebugWindowConfig), true)]
    public partial class DebugWindowConfigView
    {
        [ImportingConstructor]
        public DebugWindowConfigView(DebugWindowConfigViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
