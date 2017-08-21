using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Shell
{
    /// <summary>
    /// MainRooeShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRootShell, IsDefaultView = true)]
    public partial class MainRooeShell : UserControl
    {
        public MainRooeShell()
        {
            InitializeComponent();
        }
    }
}
