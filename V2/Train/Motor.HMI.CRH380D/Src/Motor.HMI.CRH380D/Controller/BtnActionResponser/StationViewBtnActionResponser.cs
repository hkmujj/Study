using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.StationView
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
    public class RunningResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
        }
    }
}
