using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// RegionKView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionK, IsDefaultView = true)]
    public partial class RegionKView : UserControl
    {
        public RegionKView()
        {
            InitializeComponent();
        }
    }
}
