using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.CarComfortView
{
    [Export]
    public class HVACOpenAndClose : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.CarComfortModel.CarComfortController.HVACOpenAndCloseSendData();
        }
    }

    [Export]
    public class CutInOrCutOff : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.CarComfortModel.CarComfortController.CutInAndCutOffSendData();
        }
    }

    [Export]
    public class PreparatoryConditionsOpenAndClose : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.CarComfortModel.CarComfortController.PreparatoryConditionsOpenAndCloseSendData();
        }
    }

    [Export]
    public class DriverComfortMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_司机室舒适度界面按键);
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.ClearState();
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.SetHVACButtonEnable();
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.SetAirButtonEnable();
        }
    }

    [Export]
    public class SystemMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_系统概况界面按键);
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.ClearState();
        }
    }

    [Export]
    public class MainMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_主菜单界面按键);
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.ClearState();
        }
    }

    [Export]
    public class RunAndStationResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.ClearState();
        }
    }
}
