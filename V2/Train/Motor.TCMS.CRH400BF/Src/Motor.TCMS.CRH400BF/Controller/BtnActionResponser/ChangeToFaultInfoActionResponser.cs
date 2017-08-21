using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{
   
    [Export]
    class ChangeToFaultInfoActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, "Root-故障信息界面当前故障");
        }
    }
}
