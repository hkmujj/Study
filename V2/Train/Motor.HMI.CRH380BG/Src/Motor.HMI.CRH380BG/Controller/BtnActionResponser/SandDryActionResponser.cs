using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Brake;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class SandDryActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence==Efficence.Efficent)
            {
                stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence = Efficence.NotEfficent;
                if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsTrianPipeFillWithWindEfficence==Efficence.NotEfficent)
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence==Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态八);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态四);
                    }
                }
                else
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态六);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态二);
                    }
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
                return;
            }

            if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence == Efficence.NotEfficent)
            {
                stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence = Efficence.Efficent;
                if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsTrianPipeFillWithWindEfficence == Efficence.NotEfficent)
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态七);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态三);
                    }
                }
                else
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态五);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态一);
                    }
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
}
