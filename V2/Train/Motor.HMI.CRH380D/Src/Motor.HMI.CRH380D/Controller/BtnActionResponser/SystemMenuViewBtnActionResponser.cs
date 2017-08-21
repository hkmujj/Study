using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.SystemMenuView
{
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

    [Export]
    public class HighVoltageResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_高压界面按键);
            DomainViewModel.Instacnce.Model.HighVoltyageModel.HighVoltyageController.SetButtonEnable();
        }
    }

    [Export]
    public class TractionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_牵引界面按键);
            DomainViewModel.Instacnce.Model.TractionModel.TractionController.SetButtonEnable();
        }
    }

    [Export]
    public class ACResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_交流供电界面按键);
        }
    }

    [Export]
    public class DCResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_直流供电界面按键);
        }
    }

    [Export]
    public class BreakResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_制动界面按键);
            DomainViewModel.Instacnce.Model.BreakModel.BreakController.SetButtonEnable();
        }
    }

    [Export]
    public class BogieResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_转向架界面按键);
            DomainViewModel.Instacnce.Model.BreakModel.BreakController.SetButtonEnable();
        }
    }

    [Export]
    public class AirSupplyResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_供风界面按键);
            DomainViewModel.Instacnce.Model.AirSupplyModel.AirSupplyController.SetButtonEnable();
        }
    }

    [Export]
    public class FrontResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_前部界面按键);
            DomainViewModel.Instacnce.Model.AirSupplyModel.AirSupplyController.SetButtonEnable();
        }
    }

    [Export]
    public class FireDeviceResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_火灾探测器界面按键);
        }
    }

    [Export]
    public class WCDeviceResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_卫生设备界面按键);
            DomainViewModel.Instacnce.Model.WCDeviceModel.WCDeviceController.SetButtonEnable();
        }
    }

    [Export]
    public class DriverComfortResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_司机室舒适度界面按键);
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.SetHVACButtonEnable();
            DomainViewModel.Instacnce.Model.DriverComfortModel.DriverComfortController.SetAirButtonEnable();
        }
    }

    [Export]
    public class CarComfortResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_客室舒适度界面按键);
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetCutInAndCutOffButtonEnable();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetHVACButtonEnable();
            DomainViewModel.Instacnce.Model.CarComfortModel.CarComfortController.SetTemperatureButtonEnable();
        }
    }
    [Export]
    public class DoorResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_车门界面按键);
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
        }
    }
}
