using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.WPFView.PopupViews.Title
{
    /// <summary>
    /// NormalPopupTitleView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewTitle)]
    public partial class NormalPopupTitleView : INavigationAware
    {
        public NormalPopupTitleView()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PopViewTitleText.DataContext = navigationContext.Parameters[PopViewParam.PopViewTitleKey];
            TitleText.DataContext = navigationContext.Parameters[PopViewParam.TitleKey];
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
