using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Maintain;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToMaintainActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.MaintainModelState == ModelState.Open)
            {
                NavigateTo(stateViewModel, StateKeys.Root_维护开启);
            }
            else
            {
                NavigateTo(stateViewModel, StateKeys.Root_维护关闭);
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
