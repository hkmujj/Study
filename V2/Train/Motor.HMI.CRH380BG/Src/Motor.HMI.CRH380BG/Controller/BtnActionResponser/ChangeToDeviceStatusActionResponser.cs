using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToDeviceStatusActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, StateKeys.Root_系统_设备状态);

            switch (stateViewModel.Model.ViewLocation)
            {
                case ViewLocation.Main:
                    break;
                case ViewLocation.Reserve:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
