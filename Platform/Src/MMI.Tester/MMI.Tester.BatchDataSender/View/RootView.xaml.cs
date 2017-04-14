using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Tester.BatchDataSender.Constant;

namespace MMI.Tester.BatchDataSender.View
{
    /// <summary>
    /// RootView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion)]
    public partial class RootView : UserControl
    {
        public RootView()
        {
            InitializeComponent();
        }
    }
}
