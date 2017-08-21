using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Event;
using Motor.HMI.CRH380BG.Extension;
using MMI.Facility.WPFInfrastructure.Event;
using Motor.HMI.CRH380BG.Model;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class SpeedLimit100ActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimit100 = true;

            var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
            ws.ChangedInBoolOf(InBoolKeys.Inb速度限制100kmh, true);


            NavigateTo(stateViewModel, StateKeys.Root_紧急_释放速度限制100);
        }
    }
}
