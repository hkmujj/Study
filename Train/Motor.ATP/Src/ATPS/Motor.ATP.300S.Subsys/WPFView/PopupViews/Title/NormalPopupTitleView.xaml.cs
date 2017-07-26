using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300S.Subsys.Constant;

namespace Motor.ATP._300S.Subsys.WPFView.PopupViews.Title
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
            TitleText.DataContext = navigationContext.Parameters["Title"];
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
