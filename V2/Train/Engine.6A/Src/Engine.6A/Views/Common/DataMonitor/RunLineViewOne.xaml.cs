using System.ComponentModel.Composition;
using System.Windows.Controls;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// RunLineViewOne.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.DataMonitorTabRegion, Priority = 4)]
    public partial class RunLineViewOne : UserControl, ITabItemInfoProvider
    {
        [ImportingConstructor]
        public RunLineViewOne()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "走 行 1"; } }
    }
}
