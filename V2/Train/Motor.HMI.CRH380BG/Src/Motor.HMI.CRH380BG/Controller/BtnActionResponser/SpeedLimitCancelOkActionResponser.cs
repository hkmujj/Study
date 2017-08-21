using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class SpeedLimitCancelOkActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, StateKeys.Root_紧急_释放速度限制取消限速);
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible = false;

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
