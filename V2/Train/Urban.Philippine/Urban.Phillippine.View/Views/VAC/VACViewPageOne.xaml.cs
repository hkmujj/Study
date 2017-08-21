using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views.VAC
{
    /// <summary>
    ///     VACViewPageOne.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.VACRegion, IsDefaultView = true)]
    public partial class VACViewPageOne : UserControl
    {
        public VACViewPageOne()
        {
            InitializeComponent();
        }
    }
}