using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class CancelSpeedLimitActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.DomainViewModel.Domain.Model.EmergencyModel.SpeedLimitVisible = true;

            NavigateTo(stateViewModel,StateKeys.Root_紧急_释放速度限制_取消限速 );
        }
    }
}
