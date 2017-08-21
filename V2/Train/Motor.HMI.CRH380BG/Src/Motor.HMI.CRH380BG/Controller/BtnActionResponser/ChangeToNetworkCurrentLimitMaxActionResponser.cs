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
    class ChangeToNetworkCurrentLimitMaxActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitEnable)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流_300A, false));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流_400A, false));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流限制_600A, false));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流限制_最大, false));

                stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType = NetWorkCurrentLimitType.Max;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent300AVisible = false;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent400AVisible = false;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent500AVisible = false;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent600AVisible = false;

                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制最大);
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
