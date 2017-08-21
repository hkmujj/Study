using Motor.HMI.CRH380BG.Resources.Keys;
using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToNetworkCurrentLimitActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType == NetWorkCurrentLimitType.Current300A)
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制300A);
            }
            else if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType == NetWorkCurrentLimitType.Current400A)
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制400A);
            }
            else if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType == NetWorkCurrentLimitType.Current500A)
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制500A);
            }
            else if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType == NetWorkCurrentLimitType.Current600A)
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制600A);
            }
            else
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制最大);
            }   
        }
    }
}
