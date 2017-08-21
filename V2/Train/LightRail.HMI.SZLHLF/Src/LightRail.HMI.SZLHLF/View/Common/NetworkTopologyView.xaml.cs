using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;
using System.Windows.Controls;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// NetworkTopologyView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class NetworkTopologyView : UserControl
    {
        public NetworkTopologyView()
        {
            InitializeComponent();
        }
    }
}
