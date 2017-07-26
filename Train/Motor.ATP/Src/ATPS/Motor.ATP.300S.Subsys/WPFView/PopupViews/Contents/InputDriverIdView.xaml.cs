using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300S.Subsys.Constant;
using Motor.ATP._300S.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300S.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// InputDriverIdView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class InputDriverIdView 
    {
        [ImportingConstructor]
        public InputDriverIdView(DataDriverIDPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
