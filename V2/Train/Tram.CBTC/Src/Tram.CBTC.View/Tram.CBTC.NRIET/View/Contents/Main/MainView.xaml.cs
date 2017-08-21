using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContent, IsDefaultView = false)]
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
