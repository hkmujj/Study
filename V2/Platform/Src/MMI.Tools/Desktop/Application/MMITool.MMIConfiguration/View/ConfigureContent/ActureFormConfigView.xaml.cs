using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config.Form;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent
{
    /// <summary>
    /// ActureFormConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ViewMappedConfigType(typeof(ActureFormConfigView), typeof(ActureFormConfig), false)]
    public partial class ActureFormConfigView
    {
        [ImportingConstructor]
        public ActureFormConfigView(ActureFormConfigViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
