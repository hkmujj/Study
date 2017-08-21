using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{

    [Export]
    class ChangeToBrakeInfoActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, "Root-制动界面制动信息");
        }
    }
}
