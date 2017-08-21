using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
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
    class OkAllaccelerationAlarmActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, StateKeys.Root_紧急_复位横向加速度报警限速);
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmVisible = false;
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.AccelerationAlarmType = AccelerationAlarmType.SpeedLimit;
        }

        public override void ResponseMouseDown(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub复位所有横向加速度报警, true));
        }

        public override void ResponseMouseUp(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub复位所有横向加速度报警, false));
        }
    }
}
