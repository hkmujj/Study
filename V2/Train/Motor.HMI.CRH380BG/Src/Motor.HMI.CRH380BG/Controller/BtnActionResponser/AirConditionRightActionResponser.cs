using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class AirConditionRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.AirConditionModel.SelectNum == 0)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.AirConditionModel.SelectNum = 1;
                
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.AirConditionModel.SelectNum > 2 && stateViewModel.DomainViewModel.Domain.Model.Switch.AirConditionModel.SelectNum < 10)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.AirConditionModel.SelectNum++;
            }
        }
    }
}
