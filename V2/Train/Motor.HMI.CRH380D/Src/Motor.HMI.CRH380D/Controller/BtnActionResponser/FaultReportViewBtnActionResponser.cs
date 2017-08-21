using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.FaultReportView
{
    [Export]
    public class LastPage : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }

    [Export]
    public class NextPage : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

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
