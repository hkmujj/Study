using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Contents
{
    /// <summary>
    /// EmergencyCauseView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("紧急制动原因")]
    public partial class EmergencyCauseView : UserControl
    {
        public EmergencyCauseView()
        {
            InitializeComponent();
        }
    }
}
