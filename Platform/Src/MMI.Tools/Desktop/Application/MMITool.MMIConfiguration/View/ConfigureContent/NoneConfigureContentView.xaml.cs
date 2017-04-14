using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent
{
    /// <summary>
    /// NoneConfigureContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.ConfigureEditRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class NoneConfigureContentView
    {
        public NoneConfigureContentView()
        {
            InitializeComponent();
        }
    }
}
