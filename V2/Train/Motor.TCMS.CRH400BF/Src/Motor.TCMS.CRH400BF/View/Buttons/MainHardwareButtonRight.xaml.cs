using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Buttons
{
    /// <summary>
    /// HardwareButtonRight.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainHardwareButtonRight)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainHardwareButtonRight
    {
        public MainHardwareButtonRight()
        {
            InitializeComponent();
        }
    }
}
