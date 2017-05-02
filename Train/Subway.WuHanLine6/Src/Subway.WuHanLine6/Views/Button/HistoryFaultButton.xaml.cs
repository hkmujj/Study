using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Constance;

namespace Subway.WuHanLine6.Views.Button
{
    /// <summary>
    /// HistoryFaultButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.FaultButtonContent)]
    public partial class HistoryFaultButton : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HistoryFaultButton()
        {
            InitializeComponent();
        }
    }
}