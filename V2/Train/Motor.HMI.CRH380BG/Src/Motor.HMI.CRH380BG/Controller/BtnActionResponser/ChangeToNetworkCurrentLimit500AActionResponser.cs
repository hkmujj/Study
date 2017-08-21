using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ChangeToNetworkCurrentLimit500AActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitEnable)
            {
                NavigateTo(stateViewModel, StateKeys.Root_开关_网侧电流限制500A);

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流_300A, false));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流_400A, false));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流限制_600A, true));

                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub网侧电流限制_最大, false));

                stateViewModel.DomainViewModel.Domain.Model.Switch.NetWorkCurrentLimitType = NetWorkCurrentLimitType.Current500A;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent300AVisible = false;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent400AVisible = false;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent500AVisible = true;

                stateViewModel.DomainViewModel.Domain.Model.MainData.NetCurrent600AVisible = false;
            }
            
        }
    }
}
