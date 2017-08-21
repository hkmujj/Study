using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Extension;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.Model;
using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class OkCancelSpeedLimitActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible = false;
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimit100 = false;
            var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
            ws.ChangedInBoolOf(InBoolKeys.Inb速度限制100kmh, false);

            NavigateTo(stateViewModel,StateKeys.Root_紧急_释放速度限制限速取消 );
        }

        public override void ResponseMouseDown(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub取消速度限制100kmh, true));
        }

        public override void ResponseMouseUp(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub取消速度限制100kmh, false));
        }
    }
}
