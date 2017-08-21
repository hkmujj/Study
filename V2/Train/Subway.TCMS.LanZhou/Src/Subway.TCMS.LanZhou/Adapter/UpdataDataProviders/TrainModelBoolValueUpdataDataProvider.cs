using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class TrainModelBoolValueUpdataDataProvider : UpdateDataProviderBase
    {
        private TrainModel Model
        {
            get { return ViewModel.TrainViewModel.Model; }
        }

        [ImportingConstructor]
        public TrainModelBoolValueUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            Model.RunningDirection = GetRunningDirectionState();
            Model.CarModels[1].CarBowStatus.TrainBowStatus = GetLeftBowState();
            Model.CarModels[4].CarBowStatus.TrainBowStatus = GetRightBowState();
            Model.CarModels[0].PilothouseStatus.CabStatus = GetCabHouseTc1Status();
            Model.CarModels[5].PilothouseStatus.CabStatus = GetCabHouseTc2Status();
            Model.CarModels[0].PilothouseStatus.SpaceGateStatus = GetSpaceGateTc1Status();
            Model.CarModels[5].PilothouseStatus.SpaceGateStatus = GetSpaceGateTc2Status();
            Model.CarModels[0].PilothouseStatus.EmergencyExitStatus = GetEmergencyExitTc1Status();
            Model.CarModels[5].PilothouseStatus.EmergencyExitStatus = GetEmergencyExitTc2Status();
            Model.CarTowBrakePercentData.Status = GetTowBrakeStatus();
            Model.TrainBreakWarning = GetTrainBreakWarningStatus();
        }
        private RunningDirection GetRunningDirectionState()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb方向向前))
            {
                return RunningDirection.Left;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb方向向后))
            {
                return RunningDirection.Right;
            }
            return RunningDirection.Unknown;
        }

        private BowStatus GetLeftBowState()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓动作中左))
            {
                return BowStatus.Action;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓升弓到位左))
            {
                return BowStatus.Raise;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓降弓到位左))
            {
                return BowStatus.Down;
            }
            return BowStatus.Unknown;
        }

        private BowStatus GetRightBowState()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓动作中右))
            {
                return BowStatus.Action;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓升弓到位右))
            {
                return BowStatus.Raise;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb受电弓降弓到位右))
            {
                return BowStatus.Down;
            }
            return BowStatus.Unknown;
        }

        private CarTowBrakeStatus GetTowBrakeStatus()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb牵引百分比制动状态))
            {
                return CarTowBrakeStatus.Brake;
            }
            return CarTowBrakeStatus.Tow;
        }

        private TrainBreakWarning GetTrainBreakWarningStatus()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb故障等级3))
            {
                return TrainBreakWarning.Serious;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb故障等级2))
            {
                return TrainBreakWarning.Medium;
            }
            return TrainBreakWarning.Slight;
        }

        private CabStatus GetCabHouseTc1Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb司机室激活Tc1))
            {
                return CabStatus.Active;
            }
            return CabStatus.Unactive;
        }

        private CabStatus GetCabHouseTc2Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb司机室激活Tc2))
            {
                return CabStatus.Active;
            }
            return CabStatus.Unactive;
        }

        private SpaceGateStatus GetSpaceGateTc1Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC1间壁门关闭))
            {
                return SpaceGateStatus.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC1间壁门打开))
            {
                return SpaceGateStatus.Open;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC1间壁门打开过))
            {
                return SpaceGateStatus.HasOpen;
            }
            return SpaceGateStatus.Unknow;
        }
        private SpaceGateStatus GetSpaceGateTc2Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC2间壁门关闭))
            {
                return SpaceGateStatus.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC2间壁门打开))
            {
                return SpaceGateStatus.Open;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC2间壁门打开过))
            {
                return SpaceGateStatus.HasOpen;
            }
            return SpaceGateStatus.Unknow;
        }
        private EmergencyExitStatus GetEmergencyExitTc1Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC1逃生门关闭))
            {
                return EmergencyExitStatus.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC1逃生门打开))
            {
                return EmergencyExitStatus.Open;
            }
            return EmergencyExitStatus.Unknow;
        }
        private EmergencyExitStatus GetEmergencyExitTc2Status()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC2逃生门关闭))
            {
                return EmergencyExitStatus.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.InbTC2逃生门打开))
            {
                return EmergencyExitStatus.Open;
            }
            return EmergencyExitStatus.Unknow;
        }
    }
}

