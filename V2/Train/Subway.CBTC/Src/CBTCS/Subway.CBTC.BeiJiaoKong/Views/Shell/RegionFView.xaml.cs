using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// RegionFView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionF, IsDefaultView = true)]
    public partial class RegionFView : UserControl
    {
        public RegionFView()
        {
            InitializeComponent();
        }
    }
}
