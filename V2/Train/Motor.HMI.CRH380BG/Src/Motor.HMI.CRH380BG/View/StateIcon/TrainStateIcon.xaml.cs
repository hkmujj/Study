using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.View;
using Motor.HMI.CRH380BG.Constant;

namespace Motor.HMI.CRH380BG.View.StateIcon
{
    /// <summary>
    /// MainTrainStateIcon.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TainStateIcon)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TrainStateIcon
    {
        public TrainStateIcon()
        {
            InitializeComponent();

           // FlickerManager.SetFlicking(Grid, true);
        }
    }
}
