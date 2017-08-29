using System.Windows.Controls;
using LightRail.HMI.GZYGDC.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.GZYGDC.View.Contents
{
    /// <summary>
    /// NetTopologyView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.CenterContent)]
    public partial class NetTopologyView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public NetTopologyView()
        {
            InitializeComponent();
        }
    }
}
