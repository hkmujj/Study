using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{
    [Export]
    class ChangeToEquipmentControlActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, "Root-设备控制界面设备切除");
        }
    }
}
