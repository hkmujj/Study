using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel
{
    /// <summary>
    /// ANetDetailConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.NetDetailConfigRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ANetDetailConfigView : UserControl
    {
        public ANetDetailConfigView()
        {
            InitializeComponent();
        }
    }
}
