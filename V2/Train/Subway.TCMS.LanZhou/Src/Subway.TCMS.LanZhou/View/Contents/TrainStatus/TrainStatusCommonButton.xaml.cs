using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.TCMS.LanZhou.Constant;

namespace Subway.TCMS.LanZhou.View.Contents.TrainStatus
{
    /// <summary>
    /// TrainStatusCommonButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TrainStatusButtonContent,IsDefaultView = true)]
    public partial class TrainStatusCommonButton : UserControl
    {
        public TrainStatusCommonButton()
        {
            InitializeComponent();
        }
    }
}
