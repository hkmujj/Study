using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;
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
    class AirConditionEmergencyCloseActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub所有单车空调紧急关闭, true));
        }

        public override void ResponseMouseUp(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub所有单车空调紧急关闭, false));
        }
    }
}
