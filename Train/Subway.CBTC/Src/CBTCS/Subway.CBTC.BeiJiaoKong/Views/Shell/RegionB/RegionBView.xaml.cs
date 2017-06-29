using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;
using Subway.CBTC.BeiJiaoKong.Models.RegionB;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell.RegionB
{
    /// <summary>
    /// RegionBView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionB, IsDefaultView = true)]
    public partial class RegionBView : UserControl
    {
        public RegionBView()
        {
            InitializeComponent();

            this.DegreeScaleView.ItemsSource = DegreeScaleResource.Instance.ItemCollection;
        }

        private void Emergency_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
