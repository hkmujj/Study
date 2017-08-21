using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToFanActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            

            switch (stateViewModel.DomainViewModel.Domain.Model.Switch.FanType)
            {
                    case FanType.Auto:
                    NavigateTo(stateViewModel, StateKeys.Root_开关_风扇);
                    break;
                    case FanType.Min:
                    NavigateTo(stateViewModel, StateKeys.Root_开关_风扇最小);
                    break;
                    case FanType.Max:
                    NavigateTo(stateViewModel, StateKeys.Root_开关_风扇最大);
                    break;
            }

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
