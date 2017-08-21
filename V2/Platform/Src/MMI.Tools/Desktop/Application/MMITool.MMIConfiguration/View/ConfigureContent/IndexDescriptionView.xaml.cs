using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent
{
    /// <summary>
    /// IndexDescriptionView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ViewMappedConfigType(typeof(IndexDescriptionView), typeof(IndexDescriptionConfig), true)]
    public partial class IndexDescriptionView
    {
        [ImportingConstructor]
        public IndexDescriptionView(IndexDescriptionViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
