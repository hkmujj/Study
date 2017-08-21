using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// SelecCTCSView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class SelecCTCSView 
    {
        [ImportingConstructor]
        public SelecCTCSView(SelectCTCSPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
