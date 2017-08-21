using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Resources.Keys;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.BottomView
{
    [Export]
    public class RunningBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行界面按键);
        }
    }


    [Export]
    public class AirConditionerBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_空调界面按键);
        }
    }


    [Export]
    public class FaultInfoBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_故障信息界面按键);
        }
    }


    [Export]
    public class SettingBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_设置界面按键);
        }
    }


    [Export]
    public class LastStationBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }


    [Export]
    public class NextStationBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }



    [Export]
    public class StationReporterToolBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }


    [Export]
    public class MaintenBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_维护界面按键);
        }
    }


    [Export]
    public class LanguageBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {

        }
    }


}
