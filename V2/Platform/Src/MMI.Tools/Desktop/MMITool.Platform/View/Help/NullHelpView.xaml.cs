using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Infrastructure;

namespace MMITool.Platform.View.Help
{
    /// <summary>
    /// NullHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, IsDefaultView = true)]
    public partial class NullHelpView
    {
        public NullHelpView()
        {
            InitializeComponent();
        }
    }
}
