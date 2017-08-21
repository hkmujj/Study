using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    internal class TrainIconUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public TrainIconUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = TrainModel;
            d.TrainStateIcon.ExternalPower = GetExternalPower();
            d.TrainStateIcon.AutoSafetyEquip = GetAutoSafetyEquip();
            d.TrainStateIcon.ChangePortIcon = GetChangePortIcon();
            d.TrainStateIcon.DoorStateIcon = GetDoorStateIcon();
            d.TrainStateIcon.EmptyRun = GetEmptyRun();
            d.TrainStateIcon.FireCall = GetFireCall();
            d.TrainStateIcon.PassengerCall = GetPassengerCall();
            d.TrainStateIcon.MainBreakerStateIcon = GetMainBreakerStateIcon();
            d.TrainStateIcon.OneOrEightBatteryCharger = GetOneOrEightBatteryCharger();
            d.TrainStateIcon.EightBatteryCharger = GetEightBatteryCharger();
            d.TrainStateIcon.OneOrThreeAssistInvertor = GetOneOrThreeAssistInvertor();
            d.TrainStateIcon.SixOrEightAssistInvertor = GetSixOrEightAssistInvertor();
        }

        private AssistInvertorStateIcon GetSixOrEightAssistInvertor()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb6_8车辅助逆变器仅一个工作))
            {
                return AssistInvertorStateIcon.OnlyOneRun;
            }

            if (DataService.GetInBoolOf(InbKeys.Inb6_8车辅助逆变器均故障))
            {
                return AssistInvertorStateIcon.AllFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb6_8车辅助逆变器均未工作))
            {
                return AssistInvertorStateIcon.AllNotRun;
            }
            return AssistInvertorStateIcon.AllRun;
        }

        private ShowStateIcon GetPassengerCall()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb乘客报警))
            {
                return ShowStateIcon.CanShow;
            }

            return ShowStateIcon.NotCanShow;
        }

        private AssistInvertorStateIcon GetOneOrThreeAssistInvertor()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb1_3车辅助逆变器仅一个工作))
            {
                return AssistInvertorStateIcon.OnlyOneRun;
            }

            if (DataService.GetInBoolOf(InbKeys.Inb1_3车辅助逆变器均故障))
            {
                return AssistInvertorStateIcon.AllFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb1_3车辅助逆变器均未工作))
            {
                return AssistInvertorStateIcon.AllNotRun;
            }
            return AssistInvertorStateIcon.AllRun;
        }

        private BatteryChargerStateIcon GetOneOrEightBatteryCharger()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb1_8车充电机仅一个工作))
            {
                return BatteryChargerStateIcon.OnlyOneRun;
            }

            if (DataService.GetInBoolOf(InbKeys.Inb1_8车充电机均故障))
            {
                return BatteryChargerStateIcon.AllFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb1_8车充电机均未工作))
            {
                return BatteryChargerStateIcon.AllNotRun;
            }
            return BatteryChargerStateIcon.AllRun;
        }
        private BatteryChargerStateIcon GetEightBatteryCharger()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb状态栏1_8车充电机仅一个工作08))
            {
                return BatteryChargerStateIcon.OnlyOneRun;
            }

            if (DataService.GetInBoolOf(InbKeys.Inb状态栏1_8车充电机均故障08))
            {
                return BatteryChargerStateIcon.AllFault;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb状态栏1_8车充电机均未工作08))
            {
                return BatteryChargerStateIcon.AllNotRun;
            }
            return BatteryChargerStateIcon.AllRun;
        }
        private MainBreakerStateIcon GetMainBreakerStateIcon()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb主断路器不允许闭合))
            {
                return MainBreakerStateIcon.NotCanOpen;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb主断路器允许闭合))
            {
                return MainBreakerStateIcon.CanOpen;
            }

            return MainBreakerStateIcon.None;
        }

        private ShowStateIcon GetFireCall()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb火警))
            {
                return ShowStateIcon.CanShow;
            }

            return ShowStateIcon.NotCanShow;
        }

        private ShowStateIcon GetEmptyRun()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb空转滑行))
            {
                return ShowStateIcon.CanShow;
            }

            return ShowStateIcon.NotCanShow;
        }

        private DoorStateIcon GetDoorStateIcon()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb门全关闭))
            {
                return DoorStateIcon.AllClose;
            }

            return DoorStateIcon.NotAllClose;
        }

        private ChangePortIcon GetChangePortIcon()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb换端条件满足))
            {
                return ChangePortIcon.CanChangePort;
            }

            if (DataService.GetInBoolOf(InbKeys.Inb进入换端模式))
            {
                return ChangePortIcon.EnterChangePort;
            }
            return ChangePortIcon.None;

        }

        private ShowStateIcon GetAutoSafetyEquip()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb司机脚踏))
            {
                return ShowStateIcon.CanShow;
            }

            return ShowStateIcon.NotCanShow;
        }

        private ShowStateIcon GetExternalPower()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb外接电源))
            {
                return ShowStateIcon.CanShow;
            }

            return ShowStateIcon.NotCanShow;

        }
    }
}

