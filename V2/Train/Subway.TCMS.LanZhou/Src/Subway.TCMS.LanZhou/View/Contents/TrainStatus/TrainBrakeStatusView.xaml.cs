using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.TCMS.LanZhou.Constant;

namespace Subway.TCMS.LanZhou.View.Contents.TrainStatus
{
    /// <summary>
    /// TrainBrakeStatusView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainStatusContent)]
    public partial class TrainBrakeStatusView : UserControl
    {
        public TrainBrakeStatusView()
        {
            InitializeComponent();
        }
    }
}
