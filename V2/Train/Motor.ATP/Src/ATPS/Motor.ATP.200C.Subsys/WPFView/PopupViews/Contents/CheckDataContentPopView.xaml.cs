using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._200C.Subsys.Constant;
using Motor.ATP._200C.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._200C.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// CheckDataContentPopView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class CheckDataContentPopView 
    {
        [ImportingConstructor]
        public CheckDataContentPopView(CheckDataContentPopViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
