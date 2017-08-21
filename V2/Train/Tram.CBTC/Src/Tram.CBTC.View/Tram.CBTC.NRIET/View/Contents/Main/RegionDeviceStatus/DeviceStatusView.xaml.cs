using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionDeviceStatus
{
    /// <summary>
    /// DeviceStatusView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.DeviceStatusContent,IsDefaultView = true)]
    public partial class DeviceStatusView : UserControl
    {
        public DeviceStatusView()
        {
            InitializeComponent();
        }
    }
}
