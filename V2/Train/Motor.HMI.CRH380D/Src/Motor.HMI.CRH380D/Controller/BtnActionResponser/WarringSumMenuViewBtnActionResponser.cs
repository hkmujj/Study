using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.WarringSumMenuView
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
    public class Btn1Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.高压);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn2Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.牵引);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn3Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.车门);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn4Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.控制和通讯);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn5Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.辅助供电);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn6Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.制动);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn7Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.空调);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn8Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.前部);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn9Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.蓄电池供电);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn10Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.供风);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn1R1espinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.卫生设备);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn12Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.转向架);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn1R3espinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.信息系统);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }

    [Export]
    public class Btn14Respinser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.火灾探测器);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();
            NavigateTo(StateKeys.Root_当前警报界面按键);
        }
    }


}
