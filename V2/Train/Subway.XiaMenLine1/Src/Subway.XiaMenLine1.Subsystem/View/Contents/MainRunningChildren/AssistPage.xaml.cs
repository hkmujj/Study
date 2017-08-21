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
    /// AssistPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRunningChildrenTrainRegion)]
    [TitleName("辅助电源")]
    public partial class AssistPage : IContentPage
    {
        public AssistPage()
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

        public bool IsFouce { get; private set; }

        public void ButtonGotFouce(IMMI view)
        {
            if (view.ButtonModel.AirAssistPowerState != AssistPowerState.Fault)
            {
                view.ButtonModel.AirAssistPowerState = AssistPowerState.Select;
            }
            IsFouce = true;
        }

        public void ButtonLoseFouce(IMMI view)
        {
            if (view.ButtonModel.AirAssistPowerState != AssistPowerState.Fault)
            {
                view.ButtonModel.AirAssistPowerState = AssistPowerState.Normal;
            }
            IsFouce = false;
        }
    }
}
