using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents
{
    /// <summary>
    /// StartView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContent, IsDefaultView = true)]
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
        }
    }
}
