using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.BreakTestView
{
    [Export]
    public class BreakTest : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.BreakTestModel.BreakTestController.BreakTest();
        }
    }

    [Export]
    public class AvoidSlipTest : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.BreakTestModel.BreakTestController.AvoidSlipTest();
        }
    }


    [Export]
    public class CastSandTest : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.Model.BreakTestModel.BreakTestController.CastSandTest();
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
