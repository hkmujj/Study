using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.Casco.Constant;

namespace Tram.CBTC.Casco.View.Contents.RegionPop
{
    /// <summary>
    /// PopNullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPop, IsDefaultView = true)]
    public partial class PopNullView : UserControl
    {
        public PopNullView()
        {
            InitializeComponent();
        }
    }
}
