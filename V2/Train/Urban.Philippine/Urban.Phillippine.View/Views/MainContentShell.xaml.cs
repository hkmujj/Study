using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     MainContentShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainShellRegion)]
    public partial class MainContentShell : UserControl
    {
        public MainContentShell()
        {
            InitializeComponent();
        }
    }
}