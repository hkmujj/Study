using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380D.Constant;

namespace Motor.HMI.CRH380D.View.Common
{
    /// <summary>
    /// NullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentTrainCommon1, IsDefaultView = true)]
    public partial class TrainCommon1
    {
        public TrainCommon1()
        {
            InitializeComponent();
        }
    }
}
