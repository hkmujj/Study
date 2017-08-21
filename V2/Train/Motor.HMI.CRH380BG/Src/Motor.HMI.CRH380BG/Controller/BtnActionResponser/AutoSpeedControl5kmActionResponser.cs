using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;
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
    class AutoSpeedControl5kmActionResponser : BtnActionResponserBase
    {
         /// <summary>
         /// 响应按键
         /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_2kmh连挂速度, false));

            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_5kmh, true));

            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_10kmh, false));

            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_25kmh, false));

            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub最大速度, false));

            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub自动速度控制_速度设定关闭, false));

            NavigateTo(stateViewModel, StateKeys.Root_自动速度控制_5km);

            stateViewModel.DomainViewModel.Domain.Model.MainData.AutoSpeedControlType = AutoSpeedControlType.Speed5;
        }
    }
}
