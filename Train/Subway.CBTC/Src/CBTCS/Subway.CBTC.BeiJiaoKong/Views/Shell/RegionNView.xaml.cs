using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// RegionNView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionN, IsDefaultView = true)]
    public partial class RegionNView : UserControl
    {
        public RegionNView()
        {
            InitializeComponent();
        }
    }
}
