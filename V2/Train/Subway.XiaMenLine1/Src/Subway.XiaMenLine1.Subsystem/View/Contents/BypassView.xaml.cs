using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Contents
{
    /// <summary>
    /// BypassView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("旁路")]
    public partial class BypassView
    {
        public BypassView()
        {
            InitializeComponent();
        }
    }
}
