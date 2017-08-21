using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// SleepView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.DataMonitorTabRegion, Priority = 6)]
    public partial class SleepView : ITabItemInfoProvider
    {
        [ImportingConstructor]
        public SleepView()
        {
            InitializeComponent();

            
        }

        public string HeadName { get { return "盹 睡"; } }
    }
}
