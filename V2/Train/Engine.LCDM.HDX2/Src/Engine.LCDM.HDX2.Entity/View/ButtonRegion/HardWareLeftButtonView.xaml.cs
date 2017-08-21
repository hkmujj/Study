using System.ComponentModel.Composition;
using System.Windows.Controls;
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.ViewModel;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View.ButtonRegion
{
    /// <summary>
    /// HardWareLeftButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HardwareLeftButtonRegion)]
    public partial class HardWareLeftButtonView : UserControl
    {
        [ImportingConstructor]
        public HardWareLeftButtonView(HardWareButtonViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
