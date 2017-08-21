using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.MainRunningChildren
{
    /// <summary>
    /// HightBreakerView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRunningChildrenBreakerRegion)]
    public partial class HightBreakerView : UserControl
    {
        public HightBreakerView()
        {
            InitializeComponent();
        }
    }
}
