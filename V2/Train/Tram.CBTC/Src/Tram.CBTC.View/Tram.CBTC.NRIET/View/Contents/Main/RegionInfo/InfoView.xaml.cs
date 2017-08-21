using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.NRIET.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionInfo
{
    /// <summary>
    /// InfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.InfoContent, IsDefaultView = true)]
    public partial class InfoView : UserControl
    {
        public InfoView()
        {
            InitializeComponent();
        }
    }
}
