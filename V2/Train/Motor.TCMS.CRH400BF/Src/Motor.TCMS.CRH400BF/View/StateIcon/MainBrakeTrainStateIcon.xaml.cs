using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.StateIcon
{
    /// <summary>
    /// MainTrainStateIcon.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainTrainStateIcon)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainBrakeTrainStateIcon 
    {
        public MainBrakeTrainStateIcon()
        {
            InitializeComponent();
        }
    }
}
