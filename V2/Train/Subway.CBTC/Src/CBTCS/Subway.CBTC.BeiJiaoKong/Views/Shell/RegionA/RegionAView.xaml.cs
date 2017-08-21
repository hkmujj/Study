using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// RegionAView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionA, IsDefaultView = true)]
    public partial class RegionAView : UserControl
    {
        public RegionAView()
        {
            InitializeComponent();
        }
    }
}
