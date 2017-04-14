using System.Windows;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;

namespace MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.DataProtocol
{
    /// <summary>
    /// BussnessIdAndPackageIdConfigView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.NetDetailProtocolConfigRegion)]
    public partial class BussnessIdAndPackageIdConfigView
    {
        public BussnessIdAndPackageIdConfigView()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProjectTabControl.SelectedIndex = 0;
        }
    }
}
