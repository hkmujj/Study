using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.TCMS.LanZhou.Constant;

namespace Subway.TCMS.LanZhou.View.Contents.TrainStatus
{
    /// <summary>
    /// TrainTowStatusView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainStatusContent,IsDefaultView = true)]
    public partial class TrainTowStatusView : UserControl
    {
        public TrainTowStatusView()
        {
            InitializeComponent();
        }
    }
}
