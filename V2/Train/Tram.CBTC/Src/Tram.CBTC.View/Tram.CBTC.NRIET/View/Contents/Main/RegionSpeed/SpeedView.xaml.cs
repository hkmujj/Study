using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionSpeed
{
    /// <summary>
    /// SpeedView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SpeedContent,IsDefaultView = true)]
    public partial class SpeedView : UserControl
    {
        public SpeedView()
        {
            InitializeComponent();

            
        }
    }
}
