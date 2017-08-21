using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// RegionCView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionC, IsDefaultView = true)]
    public partial class RegionCView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RegionCView()
        {
            InitializeComponent();
        }
    }
}
