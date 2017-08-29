using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.WarringCurrentView
{
    [Export]
    public class LastPage : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.OnLastPage();
        }
    }

    [Export]
    public class NextPage : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.OnNextPage();
        }
    }

    [Export]
    public class Extension : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }

    [Export]
    public class WarringSumMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            //警报记录
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventPageState(EventPageState.Warring);
            //当前事件
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetHistoryOrCurrent(true);

            NavigateTo(StateKeys.Root_警报汇总界面按键);
        }
    }

    [Export]
    public class MainMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_主菜单界面按键);
        }
    }

    [Export]
    public class RunAndStationResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
        }
    }
}
