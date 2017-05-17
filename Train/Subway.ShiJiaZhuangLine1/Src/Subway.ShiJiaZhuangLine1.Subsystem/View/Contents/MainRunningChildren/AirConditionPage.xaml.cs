using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainRunningChildren
{
    /// <summary>
    /// AirConditionPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRunningChildrenTrainRegion)]
    [TitleName("空调系统")]
    public partial class AirConditionPage : IContentPage
    {
        public AirConditionPage()
        {
            InitializeComponent();
        }

        public void ButtonGotFouce(IMMI view)
        {
            if (view.ButtonModel.AirConditionState != AirConditionState.Fault)
            {
                view.ButtonModel.AirConditionState = AirConditionState.Select;
            }
        }

        public void ButtonLoseFouce(IMMI view)
        {
            if (view.ButtonModel.AirConditionState != AirConditionState.Fault)
            {
                view.ButtonModel.AirConditionState = AirConditionState.Normal;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ButtonGotFouce(MMI);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ButtonLoseFouce(MMI);
        }

        public IMMI MMI
        {
            get { return ((ShellViewModel) DataContext).Model; }
        }
    }
}
