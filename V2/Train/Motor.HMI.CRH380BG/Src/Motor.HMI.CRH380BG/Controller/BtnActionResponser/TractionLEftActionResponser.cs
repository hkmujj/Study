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
    class TranctionLeftActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 2)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 1;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 4)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 3;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum == 6)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum = 5;
            }
            if (stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum < 11 && stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum >7)
            {
                stateViewModel.DomainViewModel.Domain.Model.Switch.TractionModel.SelectNum--;
            }
         }
    }
}
