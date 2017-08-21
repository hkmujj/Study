using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model.Domain.Maintain;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    public class PipeOneLinkActionResponser : BtnActionResponserBase
    {
        //public const double MinLight = 0;

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State==PipeState.Linked)
            {
                stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State=PipeState.UnLined;
                if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State==PipeState.Linked)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测二);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测一);
                }
                return;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State == PipeState.UnLined)
            {
                stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe1State = PipeState.Linked;
                if (stateViewModel.DomainViewModel.Domain.Model.MaintainModel.TractionHandleDetectionModel.TractionHandleDetectionPipe2State == PipeState.Linked)
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测四);
                }
                else
                {
                    NavigateTo(stateViewModel, StateKeys.Root_维护_牵引手柄检测三);
                }
            }
        }
    }
}
