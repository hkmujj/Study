using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.TCMS.LanZhou.Constant;

namespace Subway.TCMS.LanZhou.View.Contents.BreakDownInformation
{
    /// <summary>
    /// BreakDownInformationDetail.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContent)]
    public partial class BreakDownInformationDetail : UserControl
    {
        public BreakDownInformationDetail()
        {
            InitializeComponent();
        }
    }
}
