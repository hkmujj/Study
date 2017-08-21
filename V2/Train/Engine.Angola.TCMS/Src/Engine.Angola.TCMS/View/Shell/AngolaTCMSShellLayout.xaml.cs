using System.ComponentModel.Composition;
using Engine.Angola.TCMS.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.Angola.TCMS.View.Shell
{
    /// <summary>
    /// AngolaTCMSShellContent.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellRegion)]
    [Export]
    public partial class AngolaTCMSShellLayout
    {
        public AngolaTCMSShellLayout()
        {
            InitializeComponent();
        }
    }
}
