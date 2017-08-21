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
    class ChangeToCompiledReadyActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            if (stateViewModel.DomainViewModel.Domain.Model.SystemModel.CompiledVisible2 == false)
            {
                NavigateTo(stateViewModel, StateKeys.Root_系统_解编_使列车就绪解编);
            }
            else
            {
                NavigateTo(stateViewModel, StateKeys.Root_系统_解编_使列车就绪);
            }
            
        }
    }
}
