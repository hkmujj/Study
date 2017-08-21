using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Contents
{
    /// <summary>
    /// ShellContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion, IsDefaultView = false)]
    public partial class ShellContentMainContentView : UserControl
    {
        public ShellContentMainContentView()
        {
            InitializeComponent();
        }
    }
}
