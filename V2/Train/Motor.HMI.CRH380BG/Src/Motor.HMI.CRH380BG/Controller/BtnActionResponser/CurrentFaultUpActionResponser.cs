using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Model.ConfigModel;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class CurrentFaultUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            ViewModel.Value.Domain.FaultViewModel.Controller.GotoPre();
            //var mm = ViewModel.Value.Domain.FaultViewModel.Model;
            //if (mm.CurrentSelectedItemIndex == 0)
            //{
            //   return;
            //}
            //mm.CurrentSelectedItemIndex = mm.CurrentSelectedItemIndex - 1;

        }
    }
}
