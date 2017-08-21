using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.TCMS.CRH400BF.Constant;
using Motor.TCMS.CRH400BF.View.StateIcon;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{
    [Export]
    class ChangeToBrakeActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            stateViewModel.RegionManager.RequestNavigate(RegionNames.MainTrainStateIcon,
               typeof(MainBrakeTrainStateIcon).FullName);

            NavigateTo(stateViewModel, "Root-制动界面");
        }
    }
}
