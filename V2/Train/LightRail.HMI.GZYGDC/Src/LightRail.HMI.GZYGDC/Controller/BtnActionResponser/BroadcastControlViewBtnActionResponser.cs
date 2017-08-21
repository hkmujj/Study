using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Resources.Keys;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.BroadcastControlView
{
    [Export]
    public class ReturnBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行界面按键);
        }
    }


  
}
