using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionRunning
{
    /// <summary>
    /// RunningView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RunningContent, IsDefaultView = true)]
    public partial class RunningView : UserControl
    {
        public RunningView()
        {
            InitializeComponent();
        }
    }
}
