using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.LineInformation;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.Model.Domain.Car;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Constant.TrainStatus;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Controller.Domain
{
    [Export]
    public class TrainController : ControllerBase<TrainViewModel>
    {
        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            ViewModel.Model.CarModels = CreateCars();
        }

        private List<CarModel> CreateCars()
        {
            var its = GlobalParam.Instance.CarConfigs.Value;
            return its.Select((s, i) => new CarModel(s.GroupId, s.CarId, s.ShowingName)
            {
                CarBowStatus = i == 1 || i == 4 ? new CarBowStatus() : null,
                PilothouseStatus = i == 0 || i == 5 ? new CarPilothouseStatus() : null,
                DoorModel =
                    new DoorModel(i > 2 ? ItemLocationStyle.RightStyle : ItemLocationStyle.LeftStyle)
                    {
                        DoorItems = CreateDoorStates(i)
                    },
                TrainBodyViewData =
                {
                    AirCompressorStatus = CreateAirCompressorStatus(i),
                    TowState = CreateTowState(i),
                    Brake1Status = CreateBrake1Status(i),
                    Brake2Status = CreateBrake2Status(i),
                    ParkingBrakeStatus = CreateParkingBrakeStatus(i),
                    AuxiliarySystemStatus = AuxiliarySystemStatus(i)
                },
                LineInformationDatas = i == 0 || i == 5 ? new Lazy<LineInformationDatas>(() => new LineInformationDatas()
                {
                    AtpResectionStatus = CreateAtpResectionStatus(i),
                    SemiAutomaticSwitchStatus = CreateSemiAutomaticSwitchStatus(i),
                    CautionButtonStatus = CreateCautionButtonStatus(i),
                    TotalWindStatus = CreateTotalWindStatus(i),
                    ParkingBrakeReleaseStatus = CreateParkingBrakeReleaseStatus(i),
                    CommonBrakeReleaseStatus = CreateCommonBrakeReleaseStatus(i),
                    AllDoorsClosedStatus = CreateAllDoorsClosedStatus(i),
                    AtpGateEnableStatus = CreateAtpGateEnableStatus(i),
                    ZeroSpeedStatus = CreateZeroSpeedStatus(i),
                    LiftBowAllowStatus = CreateLiftBowAllowStatus(i)
                }) : null,
                RunningViewData =
                {
                    RunningViewFloatDataLazy = new Lazy<RunningViewFloatData>(() => new RunningViewFloatData()
                    {
                        BrakePressure1 = CreateBrakePressure1(i),
                        BrakePressure2 = CreateBrakePressure2(i),
                        Laden = CreateLaden(i),
                        MainPressure = CreateMainPressure(i),
                        AirCoditionTemp = CreateAirCoditionTemp(i),
                    })

                },
                AirConditionControl =
                {
                    AirConditionStatusLazy = new Lazy<AirConditionStatusData>(()=>new AirConditionStatusData()
                    {
                       AirConditionRunningStatus = AirConditionRunningStatus(i),
                        AirConditionCompressor1Status = AirConditionCompressor1Status(i),
                        AirConditionCompressor2Status = AirConditionCompressor2Status(i),
                        AirConditionCompressor3Status = AirConditionCompressor3Status(i),
                        AirConditionCompressor4Status = AirConditionCompressor4Status(i),
                        AirConditionCondensing1Status = AirConditionCondensing1Status(i),
                        AirConditionCondensing2Status = AirConditionCondensing2Status(i),
                        AirConditionCondensing3Status = AirConditionCondensing3Status(i),
                        AirConditionCondensing4Status = AirConditionCondensing4Status(i),
                        AirConditionVentilator1Status = AirConditionVentilator1Status(i),
                        AirConditionVentilator2Status = AirConditionVentilator2Status(i),
                        AirConditionVentilator3Status = AirConditionVentilator3Status(i),
                        AirConditionVentilator4Status = AirConditionVentilator4Status(i),
                        AirConditionOutsideDamper1Status = AirConditionOutsideDamper1Status(i),
                        AirConditionOutsideDamper2Status = AirConditionOutsideDamper2Status(i),
                    }),
                    AirConditionFloatDataLazy = new Lazy<AirConditionFloatData>(()=>new AirConditionFloatData()
                    {
                       AirConditionTargetTemp = AirConditionTargetTemp(i),
                       AirConditionIndoorTemp = AirConditionIndoorTemp(i),
                       AirConditionOutdoorTemp = AirConditionOutdoorTemp(i),

                    }),
                },
                
                TrainStatusDatas =
                {
                    TrainStatusTowViewLazy = new Lazy<TrainStatusTowView>(()=>new TrainStatusTowView()
                    {
                        WorkshopPowerProtectStatus = WorkshopPowerProtectStatus(i),
                        IesContactorStatus = IesContactorStatus(i),
                        WorkshopPowerStatus = WorkshopPowerStatus(i),
                        TractionSafetyStatus = TractionSafetyStatus(i),
                        HighSpeedCircuitBreaker1Status = HighSpeedCircuitBreaker1Status(i),
                        HighSpeedCircuitBreaker2Status = HighSpeedCircuitBreaker2Status(i),
                        SeparationgContactorStatus = SeparationgContactorStatus(i),
                        DirectCurrentLinkVoltage =DirectCurrentLinkVoltage(i),
                        DirectCurrentLinkCurrent = DirectCurrentLinkCurrent(i),
                        MotorCurrent= MotorCurrent(i),
                        OvervoltageStatus = OvervoltageStatus(i)
                    }),
                    TrainStatusBrakeViewLazy = new Lazy<TrainStatusBrakeView>(()=>new TrainStatusBrakeView()
                    {
                        AirBrakeAvailableStatus1 = AirBrakeAvailableStatus1(i),
                        AirBrakeAvailableStatus2 = AirBrakeAvailableStatus2(i),
                        AirBrakeApplicationStatus1 = AirBrakeApplicationStatus1(i),
                        AirBrakeApplicationStatus2 = AirBrakeApplicationStatus2(i),
                        HoldBrakeApplyStatus1 = HoldBrakeApplyStatus1(i),
                        HoldBrakeApplyStatus2 = HoldBrakeApplyStatus2(i),
                        EmergencyBrakeAvailableStatus1 = EmergencyBrakeAvailableStatus1(i),
                        EmergencyBrakeAvailableStatus2 = EmergencyBrakeAvailableStatus2(i),
                        EmergencyBrakeApplicationStatus1 = EmergencyBrakeApplicationStatus1(i),
                        EmergencyBrakeApplicationStatus2 = EmergencyBrakeApplicationStatus2(i),
                        ParkingBrakeStatus = ParkingBrakeStatus(i),
                        BrakeCylinderStatus1 = BrakeCylinderStatus1(i),
                        BrakeCylinderStatus2 = BrakeCylinderStatus2(i),
                        BogieResectionStatus1 = BogieResectionStatus1(i),
                        BogieResectionStatus2 = BogieResectionStatus2(i),
                        AirSpringPreStatus1 = AirSpringPreStatus1(i),
                        AirSpringPreStatus2 = AirSpringPreStatus2(i),
                        AirSpringPreData1 = AirSpringPreData1(i),
                        AirSpringPreData2 = AirSpringPreData2(i),

                    }),
                    TrainStatusAsisstViewLazy = new Lazy<TrainStatusAsisstView>(()=>new TrainStatusAsisstView()
                    {
                        PrechargeContactorStatus = PrechargeContactorStatus(i),
                        SeparationBreakerStatus = SeparationBreakerStatus(i),
                        DirectCurrentLinkVoltageAsisstStatus = DirectCurrentLinkVoltageAsisstStatus(i),
                        DirectCurrentLinkCurrentAssistStatus = DirectCurrentLinkCurrentAssistStatus(i),
                        AuxiliaryLoadStatus = AuxiliaryLoadStatus(i),
                        AuxiliaryLoadCurrent = AuxiliaryLoadCurrent(i),
                        AuxiliaryLoadVoltage = AuxiliaryLoadVoltage(i),
                        CurrentBatteryCharger = CurrentBatteryCharger(i),
                        BatteryBusVoltage = BatteryBusVoltage(i),
                        BatteryBusCurrent = BatteryBusCurrent(i)
                    })
                }

            }).ToList();
        }
        private CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> AirConditionOutsideDamper2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionOutsideDamperStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.OutsideDamper2Unknow)

            };
        }
        private CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> AirConditionOutsideDamper1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionOutsideDamperStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.OutsideDamper1Unknow)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator4Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionVentilatorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.VentilatorStop4)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator3Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionVentilatorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.VentilatorStop3)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionVentilatorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.VentilatorStop2)

            };
        }

        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionVentilatorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.VentilatorStop1)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing4Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCondensingMachineConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CondensingStop4)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing3Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCondensingMachineConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CondensingStop3)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCondensingMachineConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CondensingStop2)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCondensingMachineConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CondensingStop1)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor4Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCompressorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CompressorStop4)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor3Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCompressorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CompressorStop3)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCompressorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CompressorStop2)

            };
        }
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionCompressorConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CompressorStop1)

            };
        }
        private CarItem<AirConditionRunningModel, CarAirConditionRunningModelConfig> AirConditionRunningStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionRunningModelConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirConditionRunningModel, CarAirConditionRunningModelConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirConditionUnknow)

            };
        }
        private CarItem<ValidateFloatItem, CarAirConditionOutdoorTempConfig> AirConditionOutdoorTemp(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionOutdoorTempConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirConditionOutdoorTempConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, CarAirConditionIndoorTempConfig> AirConditionIndoorTemp(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionIndoorTempConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirConditionIndoorTempConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, CarAirConditionTargetTempConfig> AirConditionTargetTemp(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirConditionTargetTempConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirConditionTargetTempConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }



        private CarItem<ValidateFloatItem, BatteryBusCurrentConfig> BatteryBusCurrent(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BatteryBusCurrentConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, BatteryBusCurrentConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, BatteryBusVoltageConfig> BatteryBusVoltage(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BatteryBusVoltageConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, BatteryBusVoltageConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, CurrentBatteryChargerConfig> CurrentBatteryCharger(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CurrentBatteryChargerConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CurrentBatteryChargerConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, AuxiliaryLoadVoltageConfig> AuxiliaryLoadVoltage(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AuxiliaryLoadVoltageConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, AuxiliaryLoadVoltageConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, AuxiliaryLoadCurrentConfig> AuxiliaryLoadCurrent(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AuxiliaryLoadCurrentConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, AuxiliaryLoadCurrentConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<OpenOrClosedStatus, AuxiliaryLoadStatusConfig> AuxiliaryLoadStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AuxiliaryLoadStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, AuxiliaryLoadStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AuxiliaryLoadOpen)

            };
        }
        private CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkCurrentConfig> DirectCurrentLinkCurrentAssistStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.DirectCurrentLinkCurrentConfigAssistStatusCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkCurrentConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkVoltageConfig> DirectCurrentLinkVoltageAsisstStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.DirectCurrentLinkVoltageConfigAssistStatusCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkVoltageConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<OpenOrClosedStatus, SeparationBreakerStatusConfig> SeparationBreakerStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.SeparationBreakerStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, SeparationBreakerStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.SeparationBreakerOpen)
                
            };
        }
        private CarItem<OpenOrClosedStatus, PrechargeContactorStatusConfig> PrechargeContactorStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.PrechargeContactorStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, PrechargeContactorStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.PrechargeContactorOpen)
                
            };
        }



        private CarItem<ValidateFloatItem, AirSpringPreDataConfig> AirSpringPreData2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirSpringPreDataConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, AirSpringPreDataConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unkown2Index),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, AirSpringPreDataConfig> AirSpringPreData1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirSpringPreDataConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, AirSpringPreDataConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Unkown1Index),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> AirSpringPreStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirSpringPreStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirSpringPreStatus, AirSpringPreStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirSpringPre2Effective)

            };
        }
        private CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> AirSpringPreStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirSpringPreStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirSpringPreStatus, AirSpringPreStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirSpringPre1Effective)

            };
        }
        private CarItem<BogieResectionStatus, BogieResectionStatusConfig> BogieResectionStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BogieResectionStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BogieResectionStatus, BogieResectionStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Bogie2Resection)

            };
        }
        private CarItem<BogieResectionStatus, BogieResectionStatusConfig> BogieResectionStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BogieResectionStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BogieResectionStatus, BogieResectionStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Bogie1Resection)

            };
        }
        private CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> BrakeCylinderStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BrakeCylinderStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BrakeCylinder2Invalied)

            };
        }
        private CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> BrakeCylinderStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.BrakeCylinderStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.BrakeCylinder1Invalied)

            };
        }
        private CarItem<ParkingBrakeStatus, ParkingReleaseTrainStatusConfig> ParkingBrakeStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.ParkingReleaseTrainStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ParkingBrakeStatus, ParkingReleaseTrainStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.ParkingBrakeRelieve)

            };
        }
        private CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> EmergencyBrakeApplicationStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.EmergencyBrakeApplicationStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.EmergencyBrake2ApplicationNotapplied)

            };
        }
        private CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> EmergencyBrakeApplicationStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.EmergencyBrakeApplicationStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.EmergencyBrake1ApplicationNotapplied)

            };
        }
        private CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> EmergencyBrakeAvailableStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.EmergencyBrakeAvailableStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.EmergencyBrake2AvailableNotavailable)

            };
        }
        private CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> EmergencyBrakeAvailableStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.EmergencyBrakeAvailableStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.EmergencyBrake1AvailableNotavailable)

            };
        }
        private CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> HoldBrakeApplyStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.HoldBrakeApplyStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.HoldBrake2ApplyNotavailable)

            };
        }
        private CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> HoldBrakeApplyStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.HoldBrakeApplyStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.HoldBrake1ApplyNotavailable)

            };
        }
        private CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> AirBrakeApplicationStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirBrakeApplicationStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirBrake2ApplicationNotapplied)

            };
        }
        private CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> AirBrakeApplicationStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirBrakeApplicationStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirBrake1ApplicationNotapplied)

            };
        }
        private CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> AirBrakeAvailableStatus2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirBrakeAvailableStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirBrake2AvailableNotavailable)

            };
        }
        private CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> AirBrakeAvailableStatus1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AirBrakeAvailableStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirBrake1AvailableNotavailable)

            };
        }





        private CarItem<OvervoltageStatus, OvervoltageAbsorptionDeviceConfig> OvervoltageStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.OvervoltageAbsorptionDeviceConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OvervoltageStatus, OvervoltageAbsorptionDeviceConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.OvervoltageStop)
                
            };
        }
        private CarItem<ValidateFloatItem, MotorCurrentConfig> MotorCurrent(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.MotorCurrentConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, MotorCurrentConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, DirectCurrentLinkCurrentConfig> DirectCurrentLinkCurrent(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.DirectCurrentLinkCurrentConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, DirectCurrentLinkCurrentConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<ValidateFloatItem, DirectCurrentLinkVoltageConfig> DirectCurrentLinkVoltage(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.DirectCurrentLinkVoltageConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, DirectCurrentLinkVoltageConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }
        private CarItem<OpenOrClosedStatus, SeparationgContactorStatusConfig> SeparationgContactorStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.SeparationgContactorStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, SeparationgContactorStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.SeparationgContactorClosed)
            };
        }
        private CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus2Config> HighSpeedCircuitBreaker2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.HighSpeedCircuitBreakerStatus2ConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus2Config>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.HighSpeedCircuitBreakerClosed)
            };
        }
        private CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus1Config> HighSpeedCircuitBreaker1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.HighSpeedCircuitBreakerStatus1ConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus1Config>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.HighSpeedCircuitBreakerClosed)
            };
        }
        private CarItem<TractionSafetyStatus, TractionSafetyStatusConfig> TractionSafetyStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.TractionSafetyStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TractionSafetyStatus, TractionSafetyStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.TractionSafetyefInvalid)
            };
        }
        private CarItem<WorkshopPowerStatus, WorkshopPowerStatusConfig> WorkshopPowerStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.WorkshopPowerStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<WorkshopPowerStatus, WorkshopPowerStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.WorkshopPowerUnConnect)
            };
        }
        private CarItem<IesContactorStatus, IesContactorStatusConfig> IesContactorStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.IesContactorStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<IesContactorStatus, IesContactorStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.IesContactorUnknow)
            };
        }
        private CarItem<OpenOrClosedStatus, WorkshopPowerProtectStatusConfig> WorkshopPowerProtectStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.WorkshopPowerProtectStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<OpenOrClosedStatus, WorkshopPowerProtectStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.WorkshopPowerProtectClosed)
            };
        }
        


        private CarItem<BranchInformationStatus, AtpResectionStatusConfig> CreateAtpResectionStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAtpResectionStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, AtpResectionStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AtpResectionNormal)
            };
        }
        private CarItem<BranchInformationStatus, SemiAutomaticSwitchStatusConfig> CreateSemiAutomaticSwitchStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarSemiAutomaticSwitchStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, SemiAutomaticSwitchStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.SemiAutomaticNormal)
            };
        }
        private CarItem<BranchInformationStatus, CautionButtonStatusConfig> CreateCautionButtonStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarCautionButtonStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, CautionButtonStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CautionButtonNormal)
            };
        }
        private CarItem<BranchInformationStatus, TotalWindStatusConfig> CreateTotalWindStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarTotalWindStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, TotalWindStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.TotalWindNormal)
            };
        }
        private CarItem<BranchInformationStatus, ParkingBrakeReleaseStatusConfig> CreateParkingBrakeReleaseStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarParkingBrakeReleaseStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, ParkingBrakeReleaseStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.ParkingBrakeReleaseNormal)
            };
        }
        private CarItem<BranchInformationStatus, CommonBrakeReleaseStatusConfig> CreateCommonBrakeReleaseStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarCommonBrakeReleaseStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, CommonBrakeReleaseStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CommonBrakeReleaseNormal)
            };
        }
        private CarItem<BranchInformationStatus, AllDoorsClosedStatusConfig> CreateAllDoorsClosedStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAllDoorsClosedStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, AllDoorsClosedStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AllDoorsClosedNormal)
            };
        }
        private CarItem<BranchInformationStatus, AtpGateEnableStatusConfig> CreateAtpGateEnableStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAtpGateEnableStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, AtpGateEnableStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AtpGateEnableNormal)
            };
        }
        private CarItem<BranchInformationStatus, ZeroSpeedStatusConfig> CreateZeroSpeedStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarZeroSpeedStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, ZeroSpeedStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.ZeroSpeedNormal)
            };
        }
        private CarItem<BranchInformationStatus, LiftBowAllowStatusConfig> CreateLiftBowAllowStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarLiftBowAllowStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BranchInformationStatus, LiftBowAllowStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.LiftBowAllowNormal)
            };
        }
        private CarItem<AirCompressorStatus, AirCompressorConfig> CreateAirCompressorStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirCompressorStatusConfigCollection.Value.FirstOrDefault(
                    f => f.Index == carIndex);

            return new CarItem<AirCompressorStatus, AirCompressorConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AirCompressorUnknow)
            };
        }

        private CarItem<TowState, TowSystemConfig> CreateTowState(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarTowStateConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TowState, TowSystemConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.TractionSystemUnknow)
            };
        }

        private CarItem<BrakeStatus, BrakeStatusConfig> CreateBrake1Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakeStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeStatus, BrakeStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CommonBrakeUnknow1)
            };
        }

        private CarItem<BrakeStatus, BrakeStatusConfig> CreateBrake2Status(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakeStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<BrakeStatus, BrakeStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.CommonBrakeUnknow2)
            };
        }

        private CarItem<AuxiliarySystemStatus, AuxiliarySystemStatusConfig> AuxiliarySystemStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.AuxiliarySystemStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<AuxiliarySystemStatus, AuxiliarySystemStatusConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.AuxiliarySystemStatusUnknow)
            };
        }
        private CarItem<TrainParkingBrakeStatus, ParkingBrakeConfig> CreateParkingBrakeStatus(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarParkingBrakeStatusConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<TrainParkingBrakeStatus, ParkingBrakeConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.ParkingBrakeUnknow)
            };
        }

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> CreateBrakePressure1(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakePressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarBrakePressureConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch1UnkownIndex),
                State = new ValidateFloatItem()
            };
        }

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> CreateBrakePressure2(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarBrakePressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarBrakePressureConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.Branch2UnkownIndex),
                State = new ValidateFloatItem()
            };
        }

        private CarItem<ValidateFloatItem, CarMainPressureConfig> CreateMainPressure(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarMainPressureConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarMainPressureConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }

        private CarItem<ValidateFloatItem, CarLadenConfig> CreateLaden(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarLadenConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarLadenConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }

        private CarItem<ValidateFloatItem, CarAirConditionConfig> CreateAirCoditionTemp(int carIndex)
        {
            var itconfig =
                GlobalParam.Instance.CarAirCoditionTempConfigCollection.Value.FirstOrDefault(f => f.Index == carIndex);

            return new CarItem<ValidateFloatItem, CarAirConditionConfig>(itconfig, carIndex)
            {
                Visible = itconfig != null && !string.IsNullOrWhiteSpace(itconfig.UnkownIndex),
                State = new ValidateFloatItem()
            };
        }

        private ObservableCollection<DoorItem> CreateDoorStates(int carIndex)
        {
            var doors = GlobalParam.Instance.CarDoorConfigCollection.Value;
            var ds = new ObservableCollection<DoorItem>()
            {
                new DoorItem(1, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door1IndexOpen)
                },
                new DoorItem(2, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door2IndexOpen)
                },
                new DoorItem(3, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door3IndexOpen)
                },
                new DoorItem(4, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door4IndexOpen)
                },
                new DoorItem(5, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door5IndexOpen)
                },
                new DoorItem(6, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door6IndexOpen)
                },
                new DoorItem(7, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door7IndexOpen)
                },
                new DoorItem(8, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door8IndexOpen)
                },
                new DoorItem(9, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door9IndexOpen)
                },
                new DoorItem(10, doors[carIndex], carIndex)
                {
                    Visible = !string.IsNullOrWhiteSpace(doors[carIndex].Door10IndexOpen)
                },
            };

            return ds;
        }
    }
}