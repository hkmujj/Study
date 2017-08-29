using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Resources.Keys;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.RunningView
{
    [Export]
    public class HelpBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_帮助界面按键);
        }
    }


    [Export]
    public class NetTopologyBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_网络拓扑界面按键);
        }
    }


    [Export]
    public class EmergencyBroadcastResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EmergencyBroadcastViewModel.Controller.Initalize();

            NavigateTo(StateKeys.Root_紧急广播界面按键);
        }
    }


    [Export]
    public class StationReporterCancelResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }



    [Export]
    public class BroadcastControlResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_广播控制界面按键);
        }
    }



    [Export]
    public class ConfirmResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.ConfirmEvent();
        }
    }
}
