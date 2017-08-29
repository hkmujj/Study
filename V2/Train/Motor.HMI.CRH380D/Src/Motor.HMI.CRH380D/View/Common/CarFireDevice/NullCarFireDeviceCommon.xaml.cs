using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380D.Constant;

namespace Motor.HMI.CRH380D.View.Common.CarFireDevice
{
    /// <summary>
    /// NullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.FireDeviceCommon, IsDefaultView = true)]
    public partial class NullCarFireDeviceCommon
    {
        public NullCarFireDeviceCommon()
        {
            InitializeComponent();
        }
    }
}
