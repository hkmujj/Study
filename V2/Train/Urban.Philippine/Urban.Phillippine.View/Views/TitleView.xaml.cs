using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     TitleView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TitleRegion)]
    public partial class TitleView : UserControl
    {
        public TitleView()
        {
            InitializeComponent();
        }
    }
}