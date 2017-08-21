using System.Windows.Controls;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// RunLineViewTwo.xaml 的交互逻辑
    /// </summary>
[ViewExport(RegionName = RegionNames.DataMonitorTabRegion, Priority = 5)]
    public partial class RunLineViewTwo : UserControl, ITabItemInfoProvider
    {
        public RunLineViewTwo()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "走 行 2"; } }
    }
}
