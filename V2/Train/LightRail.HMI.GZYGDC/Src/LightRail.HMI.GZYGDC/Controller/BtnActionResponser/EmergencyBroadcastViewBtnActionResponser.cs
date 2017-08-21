using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Resources.Keys;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.EmergencyBroadcastView
{
    [Export]
    public class LastPageBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EmergencyBroadcastViewModel.Controller.LastPage();
        }
    }


    [Export]
    public class NextPageBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EmergencyBroadcastViewModel.Controller.NextPage();
        }
    }

    [Export]
    public class ReturnBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EmergencyBroadcastViewModel.Controller.ResetBroadcast();

            NavigateTo(StateKeys.Root_运行界面按键);
        }
    }
}
