using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Contents
{
    /// <summary>
    /// TractionLockView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("牵引封锁")]
    public partial class TractionLockView
    {
        public TractionLockView()
        {
            InitializeComponent();
        }
    }
}
