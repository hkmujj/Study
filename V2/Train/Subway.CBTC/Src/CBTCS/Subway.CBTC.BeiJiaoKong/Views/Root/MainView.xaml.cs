using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;

namespace Subway.CBTC.BeiJiaoKong.Views.Root
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion, IsDefaultView = true)]
    public partial class MainView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }
    }
}
