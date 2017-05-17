using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// MainContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion, IsDefaultView = true)]
    [TitleName("门状态")]
    public partial class MainRunningView : INavigationAware
    {

        public MainRunningView()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // TODO 选中状态 
            ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, ViewNames.MainRunningDoorPage);
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