using System.ComponentModel.Composition;
using System.Windows.Controls;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.ViewModel.KunMing;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// MainCircuitPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = LiuZhouRegionNames.ViewContent)]
    public partial class MainCircuitPage : UserControl
    {
        [ImportingConstructor]
        public MainCircuitPage(SS3BViewModel viewModel)
        {
            InitializeComponent();
        }
    }
}
