using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Brake;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class AirCompressorActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {


            if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence==Efficence.Efficent)
            {
                stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence = Efficence.NotEfficent;
                if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsTrianPipeFillWithWindEfficence==Efficence.NotEfficent)
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态八);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态七);
                    }
                }
                else
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态六);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态五);
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

            if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence == Efficence.NotEfficent)
            {
                stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsAirCompressorEfficence = Efficence.Efficent;
                if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsTrianPipeFillWithWindEfficence == Efficence.NotEfficent)
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态四);
                    }
                    else
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态三);
                    }
                }
                else
                {
                    if (stateViewModel.DomainViewModel.Domain.Model.BrakeModel.BrakeFunctionStatusModel.IsSandDryEfficence == Efficence.NotEfficent)
                    {
                        NavigateTo(stateViewModel, StateKeys.Root_制动状态_制动功能状态二);
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
