using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToReleaseSpeedLimitActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimit100 && stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible == false)
            {
                NavigateTo(stateViewModel,StateKeys.Root_紧急_释放速度限制100);
            }
            else if (stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimit100 == false && stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible == false)
            {
                NavigateTo(stateViewModel, StateKeys.Root_紧急_释放速度限制限速取消);
            }
            else if (stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimit100 && stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible)
            {
                NavigateTo(stateViewModel, StateKeys.Root_紧急_释放速度限制_取消限速);
            }
            else
            {
                NavigateTo(stateViewModel, StateKeys.Root_紧急_释放速度限制取消限速);
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
