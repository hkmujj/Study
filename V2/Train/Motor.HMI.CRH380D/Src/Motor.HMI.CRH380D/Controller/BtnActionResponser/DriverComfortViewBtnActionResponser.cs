using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.DriverComfortView
{
    [Export]
    public class HVACOpenAndClose : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.DriverComfortModel.DriverComfortController.HVACOpenAndCloseSendData();
        }
    }

    [Export]
    public class FootWarming : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.DriverComfortModel.DriverComfortController.FootWarmingSendData();
        }
    }


    [Export]
    public class PreparatoryConditionsOpenAndClose : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.DriverComfortModel.DriverComfortController.PreparatoryConditionsOpenAndCloseSendData();
        }
    }

    [Export]
    public class CarComfortMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_客室舒适度界面按键);
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.ClearState();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetCutInAndCutOffButtonEnable();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetHVACButtonEnable();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetTemperatureButtonEnable();
        }
    }

    [Export]
    public class SystemMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_系统概况界面按键);
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.ClearState();
        }
    }

    [Export]
    public class MainMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_主菜单界面按键);
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.ClearState();
        }
    }

    [Export]
    public class RunAndStationResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.ClearState();
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
        }
    }
}
