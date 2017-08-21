using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;

namespace Subway.ShenZhenLine9.Views.HightRegion
{
    /// <summary>
    /// HightSpeedView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HightSpeedRegion)]
    public partial class HightSpeedView : UserControl
    {
        /// <summary>
        /// 高速断路器
        /// </summary>
        public HightSpeedView()
        {
            InitializeComponent();
        }
    }
}
