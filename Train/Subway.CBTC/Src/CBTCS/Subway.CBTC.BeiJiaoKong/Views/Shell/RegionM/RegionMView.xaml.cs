using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell.RegionM
{
    /// <summary>
    /// RegionMView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionM, IsDefaultView = true)]
    public partial class RegionMView : UserControl
    {
        public RegionMView()
        {
            InitializeComponent();
        }
    }
}
