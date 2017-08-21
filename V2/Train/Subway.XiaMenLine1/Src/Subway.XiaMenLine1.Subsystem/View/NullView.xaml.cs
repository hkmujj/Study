using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View
{
    /// <summary>
    /// NullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.MainRunningChildrenBreakerRegion, "false", "0" })]
    public partial class NullView : UserControl
    {
        public NullView()
        {
            InitializeComponent();
        }
    }
}
