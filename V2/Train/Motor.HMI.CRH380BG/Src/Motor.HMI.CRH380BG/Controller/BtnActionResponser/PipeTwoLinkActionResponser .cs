using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Maintain;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    public class PipeTwoLinkActionResponser : BtnActionResponserBase
    {

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State == PipeState.Linked)
            {
                stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State = PipeState.UnLined;
                if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State==PipeState.Linked)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测三);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测一);
                }
                return;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State == PipeState.UnLined)
            {
                stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State = PipeState.Linked;
                if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State == PipeState.Linked)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测四);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测二);
                }
            }
        }
    }
}
