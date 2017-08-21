using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents
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
