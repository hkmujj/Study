using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300H.Subsys.Constant;
using Motor.ATP._300H.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300H.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// InputTelNumberView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class InputTelNumberView 
    {
        [ImportingConstructor]
        public InputTelNumberView(InputTelNumberPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
