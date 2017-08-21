using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.TractionView
{
    [Export]
    public class CutInOrCutOff : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.TractionModel.TractionController.CutInAndCutOffSendData();
        }
    }

    [Export]
    public class OpenAllAirBlower : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.TractionModel.TractionController.OpenAllAirBlowerSendData();
        }
    }

    [Export]
    public class SystemMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_系统概况界面按键);
            DomainViewModel.Instacnce.Model.TractionModel.TractionController.ClearState();
        }
    }

    [Export]
    public class MainMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_主菜单界面按键);
            DomainViewModel.Instacnce.Model.TractionModel.TractionController.ClearState();
        }
    }

    [Export]
    public class RunAndStationResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
            DomainViewModel.Instacnce.Model.TractionModel.TractionController.ClearState();
        }
    }
}
