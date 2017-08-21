using System;
using System.Collections.ObjectModel;
using System.Linq;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.LineInformation;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Model.ConfigModel;

namespace Subway.TCMS.LanZhou.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName
        {
            get { return typeof (LanZhouTCMSSubystem).Namespace; }
        }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public Lazy<ReadOnlyCollection<DoorConfig>> CarDoorConfigCollection { get; private set; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<StationConfig>> StationConfigCollection { private set; get; }

        public Lazy<ReadOnlyCollection<CarBrakePressureConfig>> CarBrakePressureConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarConfig>> CarConfigs { get; private set; }

        public Lazy<ReadOnlyCollection<CarFireAlarmStatusConfig>> CarFireAlarmStatusConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<FaultInfo>> CarFaultInfosConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<AirCompressorConfig>> CarAirCompressorStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AtpResectionStatusConfig>> CarAtpResectionStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<SemiAutomaticSwitchStatusConfig>> CarSemiAutomaticSwitchStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CautionButtonStatusConfig>> CarCautionButtonStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<TotalWindStatusConfig>> CarTotalWindStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<ParkingBrakeReleaseStatusConfig>> CarParkingBrakeReleaseStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CommonBrakeReleaseStatusConfig>> CarCommonBrakeReleaseStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AllDoorsClosedStatusConfig>> CarAllDoorsClosedStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AtpGateEnableStatusConfig>> CarAtpGateEnableStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<ZeroSpeedStatusConfig>> CarZeroSpeedStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<LiftBowAllowStatusConfig>> CarLiftBowAllowStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<TowSystemConfig>> CarTowStateConfigCollection { get; private set; }





        public Lazy<ReadOnlyCollection<WorkshopPowerProtectStatusConfig>> WorkshopPowerProtectStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<IesContactorStatusConfig>> IesContactorStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<WorkshopPowerStatusConfig>> WorkshopPowerStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<TractionSafetyStatusConfig>> TractionSafetyStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<HighSpeedCircuitBreakerStatus1Config>> HighSpeedCircuitBreakerStatus1ConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<HighSpeedCircuitBreakerStatus2Config>> HighSpeedCircuitBreakerStatus2ConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<SeparationgContactorStatusConfig>> SeparationgContactorStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<DirectCurrentLinkVoltageConfig>> DirectCurrentLinkVoltageConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<DirectCurrentLinkCurrentConfig>> DirectCurrentLinkCurrentConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<MotorCurrentConfig>> MotorCurrentConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<OvervoltageAbsorptionDeviceConfig>> OvervoltageAbsorptionDeviceConfigCollection { get; private set; }




        public Lazy<ReadOnlyCollection<AirBrakeAvailableStatusConfig>> AirBrakeAvailableStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AirBrakeApplicationStatusConfig>> AirBrakeApplicationStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<HoldBrakeApplyStatusConfig>> HoldBrakeApplyStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<EmergencyBrakeAvailableStatusConfig>> EmergencyBrakeAvailableStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<EmergencyBrakeApplicationStatusConfig>> EmergencyBrakeApplicationStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<ParkingReleaseTrainStatusConfig>> ParkingReleaseTrainStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<BrakeCylinderStatusConfig>> BrakeCylinderStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<BogieResectionStatusConfig>> BogieResectionStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AirSpringPreStatusConfig>> AirSpringPreStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AirSpringPreDataConfig>> AirSpringPreDataConfigCollection { get; private set; }



        public Lazy<ReadOnlyCollection<PrechargeContactorStatusConfig>> PrechargeContactorStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<SeparationBreakerStatusConfig>> SeparationBreakerStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<TrainAssistDirectCurrentLinkVoltageConfig>> DirectCurrentLinkVoltageConfigAssistStatusCollection { get; private set; }
        public Lazy<ReadOnlyCollection<TrainAssistDirectCurrentLinkCurrentConfig>> DirectCurrentLinkCurrentConfigAssistStatusCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AuxiliaryLoadStatusConfig>> AuxiliaryLoadStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AuxiliaryLoadCurrentConfig>> AuxiliaryLoadCurrentConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<AuxiliaryLoadVoltageConfig>> AuxiliaryLoadVoltageConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CurrentBatteryChargerConfig>> CurrentBatteryChargerConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<BatteryBusVoltageConfig>> BatteryBusVoltageConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<BatteryBusCurrentConfig>> BatteryBusCurrentConfigCollection { get; private set; }




        public Lazy<ReadOnlyCollection<AuxiliarySystemStatusConfig>> AuxiliarySystemStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<BrakeStatusConfig>> CarBrakeStatusConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<ParkingBrakeConfig>> CarParkingBrakeStatusConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarLadenConfig>> CarLadenConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarMainPressureConfig>> CarMainPressureConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionConfig>> CarAirCoditionTempConfigCollection { get; private set; }



        public Lazy<ReadOnlyCollection<CarAirConditionRunningModelConfig>> CarAirConditionRunningModelConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionCompressorConfig>> CarAirConditionCompressorConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionCondensingMachineConfig>> CarAirConditionCondensingMachineConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionVentilatorConfig>> CarAirConditionVentilatorConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionOutsideDamperStatusConfig>> CarAirConditionOutsideDamperStatusConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarAirConditionTargetTempConfig>> CarAirConditionTargetTempConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionIndoorTempConfig>> CarAirConditionIndoorTempConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirConditionOutdoorTempConfig>> CarAirConditionOutdoorTempConfigCollection { get; private set; }


        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();

            CarDoorConfigCollection =
                 new Lazy<ReadOnlyCollection<DoorConfig>>(
                     () => ExcelParser.Parser<DoorConfig>(appConfigPath).ToList().AsReadOnly());

            CarConfigs =
                new Lazy<ReadOnlyCollection<CarConfig>>(
                    () => ExcelParser.Parser<CarConfig>(appConfigPath).ToList().AsReadOnly());

            StationConfigCollection = new Lazy<ReadOnlyCollection<StationConfig>>(
                () => ExcelParser.Parser<StationConfig>(appConfigPath).ToList().AsReadOnly());

            CarBrakePressureConfigCollection =
               new Lazy<ReadOnlyCollection<CarBrakePressureConfig>>(
                   () => ExcelParser.Parser<CarBrakePressureConfig>(appConfigPath).ToList().AsReadOnly());

            CarParkingBrakeStatusConfigCollection =
               new Lazy<ReadOnlyCollection<ParkingBrakeConfig>>(
                   () => ExcelParser.Parser<ParkingBrakeConfig>(appConfigPath).ToList().AsReadOnly());

            CarAirCompressorStatusConfigCollection =
               new Lazy<ReadOnlyCollection<AirCompressorConfig>>(
                   () => ExcelParser.Parser<AirCompressorConfig>(appConfigPath).ToList().AsReadOnly());

            CarTowStateConfigCollection =
               new Lazy<ReadOnlyCollection<TowSystemConfig>>(
                   () => ExcelParser.Parser<TowSystemConfig>(appConfigPath).ToList().AsReadOnly());

            AuxiliarySystemStatusConfigCollection =
               new Lazy<ReadOnlyCollection<AuxiliarySystemStatusConfig>>(
                   () => ExcelParser.Parser<AuxiliarySystemStatusConfig>(appConfigPath).ToList().AsReadOnly());

            CarBrakeStatusConfigCollection =
               new Lazy<ReadOnlyCollection<BrakeStatusConfig>>(
                   () => ExcelParser.Parser<BrakeStatusConfig>(appConfigPath).ToList().AsReadOnly());

            CarLadenConfigCollection =
                new Lazy<ReadOnlyCollection<CarLadenConfig>>(
                    () => ExcelParser.Parser<CarLadenConfig>(appConfigPath).ToList().AsReadOnly());

            CarMainPressureConfigCollection =
                new Lazy<ReadOnlyCollection<CarMainPressureConfig>>(
                    () => ExcelParser.Parser<CarMainPressureConfig>(appConfigPath).ToList().AsReadOnly());

            CarAirCoditionTempConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirConditionConfig>>(
                    () => ExcelParser.Parser<CarAirConditionConfig>(appConfigPath).ToList().AsReadOnly());
            CarCommonBrakeReleaseStatusConfigCollection =
                new Lazy<ReadOnlyCollection<CommonBrakeReleaseStatusConfig>>(
                    () => ExcelParser.Parser<CommonBrakeReleaseStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarAllDoorsClosedStatusConfigCollection =
               new Lazy<ReadOnlyCollection<AllDoorsClosedStatusConfig>>(
                   () => ExcelParser.Parser<AllDoorsClosedStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarCautionButtonStatusConfigCollection =
                new Lazy<ReadOnlyCollection<CautionButtonStatusConfig>>(
                    () => ExcelParser.Parser<CautionButtonStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarTotalWindStatusConfigCollection =
                new Lazy<ReadOnlyCollection<TotalWindStatusConfig>>(
                    () => ExcelParser.Parser<TotalWindStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarParkingBrakeReleaseStatusConfigCollection =
                new Lazy<ReadOnlyCollection<ParkingBrakeReleaseStatusConfig>>(
                    () => ExcelParser.Parser<ParkingBrakeReleaseStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarAtpGateEnableStatusConfigCollection =
                new Lazy<ReadOnlyCollection<AtpGateEnableStatusConfig>>(
                    () => ExcelParser.Parser<AtpGateEnableStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarZeroSpeedStatusConfigCollection =
                new Lazy<ReadOnlyCollection<ZeroSpeedStatusConfig>>(
                    () => ExcelParser.Parser<ZeroSpeedStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarAtpResectionStatusConfigCollection =
                new Lazy<ReadOnlyCollection<AtpResectionStatusConfig>>(
                    () => ExcelParser.Parser<AtpResectionStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarSemiAutomaticSwitchStatusConfigCollection =
                new Lazy<ReadOnlyCollection<SemiAutomaticSwitchStatusConfig>>(
                    () => ExcelParser.Parser<SemiAutomaticSwitchStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarLiftBowAllowStatusConfigCollection =
                new Lazy<ReadOnlyCollection<LiftBowAllowStatusConfig>>(
                    () => ExcelParser.Parser<LiftBowAllowStatusConfig>(appConfigPath).ToList().AsReadOnly());



            WorkshopPowerProtectStatusConfigCollection =
                new Lazy<ReadOnlyCollection<WorkshopPowerProtectStatusConfig>>(
                    () => ExcelParser.Parser<WorkshopPowerProtectStatusConfig>(appConfigPath).ToList().AsReadOnly());
            IesContactorStatusConfigCollection =
                new Lazy<ReadOnlyCollection<IesContactorStatusConfig>>(
                    () => ExcelParser.Parser<IesContactorStatusConfig>(appConfigPath).ToList().AsReadOnly());
            WorkshopPowerStatusConfigCollection =
               new Lazy<ReadOnlyCollection<WorkshopPowerStatusConfig>>(
                   () => ExcelParser.Parser<WorkshopPowerStatusConfig>(appConfigPath).ToList().AsReadOnly());
            TractionSafetyStatusConfigCollection =
                new Lazy<ReadOnlyCollection<TractionSafetyStatusConfig>>(
                    () => ExcelParser.Parser<TractionSafetyStatusConfig>(appConfigPath).ToList().AsReadOnly());
            HighSpeedCircuitBreakerStatus1ConfigCollection =
                new Lazy<ReadOnlyCollection<HighSpeedCircuitBreakerStatus1Config>>(
                    () => ExcelParser.Parser<HighSpeedCircuitBreakerStatus1Config>(appConfigPath).ToList().AsReadOnly());
            HighSpeedCircuitBreakerStatus2ConfigCollection =
                    new Lazy<ReadOnlyCollection<HighSpeedCircuitBreakerStatus2Config>>(
                        () => ExcelParser.Parser<HighSpeedCircuitBreakerStatus2Config>(appConfigPath).ToList().AsReadOnly());
            SeparationgContactorStatusConfigCollection =
                new Lazy<ReadOnlyCollection<SeparationgContactorStatusConfig>>(
                    () => ExcelParser.Parser<SeparationgContactorStatusConfig>(appConfigPath).ToList().AsReadOnly());
            DirectCurrentLinkVoltageConfigCollection =
              new Lazy<ReadOnlyCollection<DirectCurrentLinkVoltageConfig>>(
                  () => ExcelParser.Parser<DirectCurrentLinkVoltageConfig>(appConfigPath).ToList().AsReadOnly());
            DirectCurrentLinkCurrentConfigCollection =
                new Lazy<ReadOnlyCollection<DirectCurrentLinkCurrentConfig>>(
                    () => ExcelParser.Parser<DirectCurrentLinkCurrentConfig>(appConfigPath).ToList().AsReadOnly());
            MotorCurrentConfigCollection =
                    new Lazy<ReadOnlyCollection<MotorCurrentConfig>>(
                        () => ExcelParser.Parser<MotorCurrentConfig>(appConfigPath).ToList().AsReadOnly());
            OvervoltageAbsorptionDeviceConfigCollection =
                new Lazy<ReadOnlyCollection<OvervoltageAbsorptionDeviceConfig>>(
                    () => ExcelParser.Parser<OvervoltageAbsorptionDeviceConfig>(appConfigPath).ToList().AsReadOnly());



            PrechargeContactorStatusConfigCollection =
               new Lazy<ReadOnlyCollection<PrechargeContactorStatusConfig>>(
                   () => ExcelParser.Parser<PrechargeContactorStatusConfig>(appConfigPath).ToList().AsReadOnly());
            SeparationBreakerStatusConfigCollection =
                new Lazy<ReadOnlyCollection<SeparationBreakerStatusConfig>>(
                    () => ExcelParser.Parser<SeparationBreakerStatusConfig>(appConfigPath).ToList().AsReadOnly());
            DirectCurrentLinkVoltageConfigAssistStatusCollection =
              new Lazy<ReadOnlyCollection<TrainAssistDirectCurrentLinkVoltageConfig>>(
                  () => ExcelParser.Parser<TrainAssistDirectCurrentLinkVoltageConfig>(appConfigPath).ToList().AsReadOnly());
            DirectCurrentLinkCurrentConfigAssistStatusCollection =
                new Lazy<ReadOnlyCollection<TrainAssistDirectCurrentLinkCurrentConfig>>(
                    () => ExcelParser.Parser<TrainAssistDirectCurrentLinkCurrentConfig>(appConfigPath).ToList().AsReadOnly());
            AuxiliaryLoadStatusConfigCollection =
                new Lazy<ReadOnlyCollection<AuxiliaryLoadStatusConfig>>(
                    () => ExcelParser.Parser<AuxiliaryLoadStatusConfig>(appConfigPath).ToList().AsReadOnly());
            AuxiliaryLoadCurrentConfigCollection =
                    new Lazy<ReadOnlyCollection<AuxiliaryLoadCurrentConfig>>(
                        () => ExcelParser.Parser<AuxiliaryLoadCurrentConfig>(appConfigPath).ToList().AsReadOnly());
            AuxiliaryLoadVoltageConfigCollection =
                new Lazy<ReadOnlyCollection<AuxiliaryLoadVoltageConfig>>(
                    () => ExcelParser.Parser<AuxiliaryLoadVoltageConfig>(appConfigPath).ToList().AsReadOnly());
            CurrentBatteryChargerConfigCollection =
              new Lazy<ReadOnlyCollection<CurrentBatteryChargerConfig>>(
                  () => ExcelParser.Parser<CurrentBatteryChargerConfig>(appConfigPath).ToList().AsReadOnly());
            BatteryBusVoltageConfigCollection =
              new Lazy<ReadOnlyCollection<BatteryBusVoltageConfig>>(
                  () => ExcelParser.Parser<BatteryBusVoltageConfig>(appConfigPath).ToList().AsReadOnly());
            BatteryBusCurrentConfigCollection =
                new Lazy<ReadOnlyCollection<BatteryBusCurrentConfig>>(
                    () => ExcelParser.Parser<BatteryBusCurrentConfig>(appConfigPath).ToList().AsReadOnly());




            AirBrakeAvailableStatusConfigCollection =
             new Lazy<ReadOnlyCollection<AirBrakeAvailableStatusConfig>>(
                 () => ExcelParser.Parser<AirBrakeAvailableStatusConfig>(appConfigPath).ToList().AsReadOnly());
            AirBrakeApplicationStatusConfigCollection =
                new Lazy<ReadOnlyCollection<AirBrakeApplicationStatusConfig>>(
                    () => ExcelParser.Parser<AirBrakeApplicationStatusConfig>(appConfigPath).ToList().AsReadOnly());
            HoldBrakeApplyStatusConfigCollection =
             new Lazy<ReadOnlyCollection<HoldBrakeApplyStatusConfig>>(
                 () => ExcelParser.Parser<HoldBrakeApplyStatusConfig>(appConfigPath).ToList().AsReadOnly());
            EmergencyBrakeAvailableStatusConfigCollection =
                new Lazy<ReadOnlyCollection<EmergencyBrakeAvailableStatusConfig>>(
                    () => ExcelParser.Parser<EmergencyBrakeAvailableStatusConfig>(appConfigPath).ToList().AsReadOnly());
            EmergencyBrakeApplicationStatusConfigCollection =
                new Lazy<ReadOnlyCollection<EmergencyBrakeApplicationStatusConfig>>(
                    () => ExcelParser.Parser<EmergencyBrakeApplicationStatusConfig>(appConfigPath).ToList().AsReadOnly());
            ParkingReleaseTrainStatusConfigCollection =
                    new Lazy<ReadOnlyCollection<ParkingReleaseTrainStatusConfig>>(
                        () => ExcelParser.Parser<ParkingReleaseTrainStatusConfig>(appConfigPath).ToList().AsReadOnly());
            BrakeCylinderStatusConfigCollection =
                new Lazy<ReadOnlyCollection<BrakeCylinderStatusConfig>>(
                    () => ExcelParser.Parser<BrakeCylinderStatusConfig>(appConfigPath).ToList().AsReadOnly());
            BogieResectionStatusConfigCollection =
              new Lazy<ReadOnlyCollection<BogieResectionStatusConfig>>(
                  () => ExcelParser.Parser<BogieResectionStatusConfig>(appConfigPath).ToList().AsReadOnly());
            AirSpringPreStatusConfigCollection =
              new Lazy<ReadOnlyCollection<AirSpringPreStatusConfig>>(
                  () => ExcelParser.Parser<AirSpringPreStatusConfig>(appConfigPath).ToList().AsReadOnly());
            AirSpringPreDataConfigCollection =
              new Lazy<ReadOnlyCollection<AirSpringPreDataConfig>>(
                  () => ExcelParser.Parser<AirSpringPreDataConfig>(appConfigPath).ToList().AsReadOnly());


            CarAirConditionRunningModelConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirConditionRunningModelConfig>>(
                    () => ExcelParser.Parser<CarAirConditionRunningModelConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionCompressorConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirConditionCompressorConfig>>(
                    () => ExcelParser.Parser<CarAirConditionCompressorConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionCondensingMachineConfigCollection =
                    new Lazy<ReadOnlyCollection<CarAirConditionCondensingMachineConfig>>(
                        () => ExcelParser.Parser<CarAirConditionCondensingMachineConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionVentilatorConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirConditionVentilatorConfig>>(
                    () => ExcelParser.Parser<CarAirConditionVentilatorConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionOutsideDamperStatusConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirConditionOutsideDamperStatusConfig>>(
                    () => ExcelParser.Parser<CarAirConditionOutsideDamperStatusConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionTargetTempConfigCollection =
              new Lazy<ReadOnlyCollection<CarAirConditionTargetTempConfig>>(
                  () => ExcelParser.Parser<CarAirConditionTargetTempConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionIndoorTempConfigCollection =
              new Lazy<ReadOnlyCollection<CarAirConditionIndoorTempConfig>>(
                  () => ExcelParser.Parser<CarAirConditionIndoorTempConfig>(appConfigPath).ToList().AsReadOnly());
            CarAirConditionOutdoorTempConfigCollection =
              new Lazy<ReadOnlyCollection<CarAirConditionOutdoorTempConfig>>(
                  () => ExcelParser.Parser<CarAirConditionOutdoorTempConfig>(appConfigPath).ToList().AsReadOnly());


            CarFireAlarmStatusConfigCollection =
              new Lazy<ReadOnlyCollection<CarFireAlarmStatusConfig>>(
                  () => ExcelParser.Parser<CarFireAlarmStatusConfig>(appConfigPath).ToList().AsReadOnly());


            CarFaultInfosConfigCollection = new Lazy<ReadOnlyCollection<FaultInfo>>(
                  () => ExcelParser.Parser<FaultInfo>(appConfigPath).ToList().AsReadOnly()); 
        }
    }
}