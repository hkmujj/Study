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
    public class LightUpActionResponser : BtnActionResponserBase
    {
        public const double MaxLight = 100;

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.DomainViewModel.Other.Model.Light =
                Math.Min(stateViewModel.DomainViewModel.Other.Model.Light + 10, MaxLight);
        }
    }
}
