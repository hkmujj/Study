using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300H.Subsys.Constant;
using Motor.ATP._300H.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300H.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// InputTrainDataView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class InputTrainDataView 
    {
        [ImportingConstructor]
        public InputTrainDataView(InputTrainDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
