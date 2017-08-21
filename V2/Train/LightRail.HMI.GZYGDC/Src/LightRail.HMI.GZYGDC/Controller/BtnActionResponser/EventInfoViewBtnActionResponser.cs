using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Resources.Keys;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.EventInfoView
{
    [Export]
    public class LastPageBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.LastPage();
        }
    }


    [Export]
    public class NextPageBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.NextPage();
        }
    }

    [Export]
    public class HistoryFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.ShowHistoryEvent();
        }
    }

    [Export]
    public class TimeBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }


    [Export]
    public class UpBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }


    [Export]
    public class AllFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.SetEventLevel("Normal");
        }
    }


    [Export]
    public class GraveFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.SetEventLevel("Grave");
        }
    }


    [Export]
    public class MediumFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.SetEventLevel("Medium");
        }
    }


    [Export]
    public class LightFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.SetEventLevel("Light");
        }
    }


    [Export]
    public class TipFaultBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.EventInfoViewModel.Controller.SetEventLevel("Tip");
        }
    }
}
