using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380BG.Constant;
using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380BG.View.Buttons
{
    /// <summary>
    /// HardwareButtonDown.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellButtonDown)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class HardwareButtonDown
    {
        public HardwareButtonDown()
        {
            InitializeComponent();
        }
    }
}
