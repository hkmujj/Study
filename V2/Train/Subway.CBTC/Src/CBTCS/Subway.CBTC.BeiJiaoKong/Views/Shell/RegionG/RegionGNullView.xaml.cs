using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell.RegionG
{
    /// <summary>
    /// RegionGNullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionG, IsDefaultView = true)]
    public partial class RegionGNullView : UserControl
    {
        public RegionGNullView()
        {
            InitializeComponent();
        }
    }
}
