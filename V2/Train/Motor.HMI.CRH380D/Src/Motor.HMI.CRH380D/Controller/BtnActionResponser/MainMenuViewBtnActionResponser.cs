﻿using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser.MainMenuView
{
    [Export]
    public class RunAndStationResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行车站界面按键);
            DomainViewModel.Instacnce.Model.DoorModel.DoorController.SetButtonEnable();
        }
    }

    [Export]
    public class WarringRecordResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            //警报记录
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventPageState(EventPageState.Warring);
            //历史事件
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetHistoryOrCurrent(false);
            //所有系统
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventSystem(EventSystemState.默认);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();

            NavigateTo(StateKeys.Root_警报记录界面按键);
        }
    }
    
    [Export]
    public class WarringSumMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            //警报记录
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventPageState(EventPageState.Warring);
            //当前事件
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetHistoryOrCurrent(true);

            NavigateTo(StateKeys.Root_警报汇总界面按键);
        }
    }

    [Export]
    public class FaultReportResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            //故障报告
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetEventPageState(EventPageState.Fault);
            //历史事件
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.SetHistoryOrCurrent(false);
            DomainViewModel.Instacnce.Model.EventInfoModel.EventInfoController.Update();

            NavigateTo(StateKeys.Root_故障报告界面按键);
        }
    }


    [Export]
    public class SystemMenuResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_系统概况界面按键);
        }
    }

    [Export]
    public class ActivateResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_激活界面按键);
        }
    }

    [Export]
    public class InterLockResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_互锁界面按键);
        }
    }

    [Export]
    public class TrainStatusResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_列车状态界面按键);
        }
    }

    [Export]
    public class TractionAndBreakResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_牵引制动界面按键);
        }
    }

    [Export]
    public class SettingResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_设置界面按键);
        }
    }

    [Export]
    public class BreakTestResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_制动试验概况界面按键);
        }
    }

    [Export]
    public class HandleTestResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_手柄测试界面按键);
        }
    }
    
}
