using Motor.HMI.CRH380BG.Model.BtnStragy;
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
    class OkComPiledActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            NavigateTo(stateViewModel, StateKeys.Root_系统_解编_正在解编);
            if (ViewManager.Value.MainViewModel.StateViewModel.Model.CurrentStateInterface.Id == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_正在解编))
            {
                ViewManager.Value.MainViewModel.StateViewModel.Model.CompiledVisible3 = true;
            }

            if (ViewManager.Value.ReserveViewModel.StateViewModel.Model.CurrentStateInterface.Id == StateInterfaceKey.Parser(StateKeys.Root_系统_解编_正在解编))
            {
                ViewManager.Value.ReserveViewModel.StateViewModel.Model.CompiledVisible3 = true;
            }
        }
    }
}
