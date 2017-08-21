using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

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

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var data = ServiceLocator.Current.GetInstance<ShellViewModel>();
            if (data.Model.MainModel.BrakeModel == BrakeModel.EmergencyInfliction)
            {
                data.Controller.ShellContentNavigateCommand.Execute(ViewNames.EmergencyBrakeCauseView);
            }
        }
    }
}