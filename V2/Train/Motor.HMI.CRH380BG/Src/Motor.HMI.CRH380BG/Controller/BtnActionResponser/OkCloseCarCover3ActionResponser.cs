using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.Model.Domain.System.GroupHangs;
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
    class OkCloseCarCover3ActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            //stateViewModel.DomainViewModel.Domain.Model.SystemModel.GroupHangsModel.ConfirmInfo1Visible = false;
            //stateViewModel.DomainViewModel.Domain.Model.SystemModel.GroupHangsModel.ConfirmInfo2Visible = false;
        }
        
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            var group = ViewModel.Value.Domain.Model.SystemModel.GroupHangsModel;
            if (group.ConfirmInfo1Visible == true
                && group.ConfirmInfo2Visible == false)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub请求动车组1前车钩关闭, true));
            }
            else if (group.ConfirmInfo1Visible == false
                && group.ConfirmInfo2Visible == true)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub请求动车组1后车钩关闭, true));
            }
            else
            {
                return;
            }
        }

        public override void ResponseMouseUp(CommandParameter parameter)
        {
            var group = ViewModel.Value.Domain.Model.SystemModel.GroupHangsModel;
            if (group.ConfirmInfo1Visible == true
                && group.ConfirmInfo2Visible == false)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub请求动车组1前车钩关闭, false));
                group.ConfirmInfo1Visible = false;
            }
            else if (group.ConfirmInfo1Visible == false
                && group.ConfirmInfo2Visible == true)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OutBoolKeys.Oub请求动车组1后车钩关闭, false));
                group.ConfirmInfo2Visible = false;
            }
            return;
        }
    }
}
