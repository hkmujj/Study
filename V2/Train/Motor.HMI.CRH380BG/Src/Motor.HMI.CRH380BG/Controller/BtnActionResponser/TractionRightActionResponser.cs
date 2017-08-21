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
    class TractionRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
           if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 1)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 2;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 3)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 4;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 5)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 6;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum < 10 && stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum >6)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum++;
            }
         }
    }
}
