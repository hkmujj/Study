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
    /// FramHispeedPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRunningChildrenTrainRegion)]
    [TitleName("受电弓/车间电源/高速断路器")]
    public partial class FramHispeedPage : IContentPage
    {
        public FramHispeedPage()
        {
            InitializeComponent();
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

        public void ButtonGotFouce(IMMI view)
        {
            if (view.ButtonModel.FrsmHighSpeed != FrsmHighSpeed.Fault)
            {
                view.ButtonModel.FrsmHighSpeed = FrsmHighSpeed.Select;
            }
        }

        public void ButtonLoseFouce(IMMI view)
        {
            if (view.ButtonModel.FrsmHighSpeed != FrsmHighSpeed.Fault)
            {
                view.ButtonModel.FrsmHighSpeed = FrsmHighSpeed.Normal;
            }
        }
    }
}
