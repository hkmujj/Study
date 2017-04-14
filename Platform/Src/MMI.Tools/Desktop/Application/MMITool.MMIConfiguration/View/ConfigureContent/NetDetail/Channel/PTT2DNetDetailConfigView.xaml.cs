using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel
{
    /// <summary>
    /// PTT2DNetDetailConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.NetDetailConfigRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PTT2DNetDetailConfigView : UserControl
    {
        public PTT2DNetDetailConfigView()
        {
            InitializeComponent();
        }
    }
}
