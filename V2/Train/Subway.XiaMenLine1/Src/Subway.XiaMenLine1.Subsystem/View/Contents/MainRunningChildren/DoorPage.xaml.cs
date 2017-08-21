using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.MainRunningChildren
{
    /// <summary>
    /// DoorPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRunningChildrenTrainRegion, IsDefaultView = true)]
    [TitleName("主界面")]
    public partial class DoorPage : IContentPage
    {
        public DoorPage()
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
            get { return ((ShellViewModel)DataContext).Model; }
        }

        public bool IsFouce { get; private set; }

        public void ButtonGotFouce(IMMI view)
        {
            if (view.ButtonModel.DoorStatus != DoorStatus.FaultDispaly &&
                view.ButtonModel.DoorStatus != DoorStatus.CutDisplay)
            {
                view.ButtonModel.DoorStatus = DoorStatus.SelectDisplay;
            }
            IsFouce = true;
        }

        public void ButtonLoseFouce(IMMI view)
        {
            if (view.ButtonModel.DoorStatus != DoorStatus.FaultDispaly &&
                view.ButtonModel.DoorStatus != DoorStatus.CutDisplay)
            {
                view.ButtonModel.DoorStatus = DoorStatus.NormalDisplay;
            }
            IsFouce = false;
        }
    }
}
