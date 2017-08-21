using System;
using System.Collections.ObjectModel;
using System.Linq;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.ConfigModel;

namespace Urban.GuiYang.DDU.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName
        {
            get { return typeof (GuiYangDDUSubystem).Namespace; }
        }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public Lazy<ReadOnlyCollection<StateInterfaceItem>> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarBaseConfig>> CarBaseConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarDoorConfig>> CarDoorConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarPECUConfig>> CarPECUConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarFireConfig>> CarFireConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarDoorLockStateConfig>> CarDoorLockStateConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarTowInvertorConfig>> CarTowInvertorConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarAssistInvertorConfig>> CarAssistInvertorConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarNormalBrakeConfig>> CarNormalBrakeConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarParkingBrakeConfig>> CarParkingBrakeConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarHightSwitchConfig>> CarHightSwitchConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarElveConfig>> CarElveConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarGroundConnectConfig>> CarGroundConnectConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarMotoTemperatureConfig>> CarMotoTemperatureConfigCollection { get; private set;
        }

        public Lazy<ReadOnlyCollection<CarTowBrakeKNConfig>> CarTowBrakeKNConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirCompreeConfig>> CarAirCompreeConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarBogieIsolationValveConfig>> CarBogieIsolationValveConfigCollection { get;
            private set; }

        public Lazy<ReadOnlyCollection<CarEmergBrakeRelayConfig>> CarEmergBrakeRelayConfigCollection { get; private set;
        }

        public Lazy<ReadOnlyCollection<CarPackingBrakeIsolationValveConfig>>
            CarPackingBrakeIsolationValveConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAirSpringPreesureConfig>> CarAirSpringPreesureConfigCollection { get;
            private set; }

        public Lazy<ReadOnlyCollection<StationConfig>> StationConfigCollection { private set; get; }

        public Lazy<ReadOnlyCollection<CarBrakePressureConfig>> CarBrakePressureConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarMainPressureConfig>> CarMainPressureConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarWeightConfig>> CarWeightConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarLadenConfig>> CarLadenConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<EmergBroadcastConfig>> EmergBroadcastConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarPowerSwitchConfig>> CarPowerSwitchConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarBatteryChargerStateConfig>> CarBatteryChargerStateConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarStorageBatterytemperatureConfig>> CarStorageBatterytemperatureConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarAssistLoadSwitchConfig>> CarAssistLoadSwitchConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarStorageBatteryAConfig>> CarStorageBatteryAConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarGroup1NewAirValveConfig>> CarGroup1NewAirValveConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarGroup1BackAirValveConfig>> CarGroup1BackAirValveConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarGroup2NewAirValveConfig>> CarGroup2NewAirValveConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarGroup2BackAirValveConfig>> CarGroup2BackAirValveConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarAlertBypassConfig>> CarAlertBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarMainWindLowPresByPassConfig>> CarMainWindLowPresByPassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarParkingRelaseBypassConfig>> CarParkingRelaseBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarNormalBrakeBypassConfig>> CarNormalBrakeBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarDoorBypassConfig>> CarDoorBypassConfigCollection { get; private set; }

        public Lazy<ReadOnlyCollection<CarCoulplingStateConfig>> CarCoulplingStateConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarPantographEnableBypassConfig>> CarPantographEnableBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarATCCutOffBypassConfig>> CarATCCutOffBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarDoorEnableBypassConfig>> CarDoorEnableBypassConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarKeyBypassConfig>> CarKeyBypassConfigCollection { get; private set; }
        
        public Lazy<ReadOnlyCollection<CarAirConditionConfig>> CarAirConditionConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarTemperatureConfig>> CarTemperatureConfigCollection { get; private set; }
        public Lazy<ReadOnlyCollection<CarControlModelConfig>> CarControlModelConfigCollection { get; private set; }
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
            StateInterfaceCollection =
                new Lazy<ReadOnlyCollection<StateInterfaceItem>>(
                    () => ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly());

            CarBaseConfigCollection =
                new Lazy<ReadOnlyCollection<CarBaseConfig>>(
                    () => ExcelParser.Parser<CarBaseConfig>(appConfigPath).ToList().AsReadOnly());

            CarDoorConfigCollection =
                new Lazy<ReadOnlyCollection<CarDoorConfig>>(
                    () => ExcelParser.Parser<CarDoorConfig>(appConfigPath).ToList().AsReadOnly());

            CarPECUConfigCollection =
                new Lazy<ReadOnlyCollection<CarPECUConfig>>(
                    () => ExcelParser.Parser<CarPECUConfig>(appConfigPath).ToList().AsReadOnly());

            CarFireConfigCollection =
                new Lazy<ReadOnlyCollection<CarFireConfig>>(
                    () => ExcelParser.Parser<CarFireConfig>(appConfigPath).ToList().AsReadOnly());

            CarDoorLockStateConfigCollection =
                new Lazy<ReadOnlyCollection<CarDoorLockStateConfig>>(
                    () => ExcelParser.Parser<CarDoorLockStateConfig>(appConfigPath).ToList().AsReadOnly());

            CarTowInvertorConfigCollection =
                new Lazy<ReadOnlyCollection<CarTowInvertorConfig>>(
                    () => ExcelParser.Parser<CarTowInvertorConfig>(appConfigPath).ToList().AsReadOnly());

            CarAssistInvertorConfigCollection =
                new Lazy<ReadOnlyCollection<CarAssistInvertorConfig>>(
                    () => ExcelParser.Parser<CarAssistInvertorConfig>(appConfigPath).ToList().AsReadOnly());

            CarNormalBrakeConfigCollection =
                new Lazy<ReadOnlyCollection<CarNormalBrakeConfig>>(
                    () => ExcelParser.Parser<CarNormalBrakeConfig>(appConfigPath).ToList().AsReadOnly());

            CarParkingBrakeConfigCollection =
                new Lazy<ReadOnlyCollection<CarParkingBrakeConfig>>(
                    () => ExcelParser.Parser<CarParkingBrakeConfig>(appConfigPath).ToList().AsReadOnly());

            CarHightSwitchConfigCollection =
                new Lazy<ReadOnlyCollection<CarHightSwitchConfig>>(
                    () => ExcelParser.Parser<CarHightSwitchConfig>(appConfigPath).ToList().AsReadOnly());

            CarGroundConnectConfigCollection =
                new Lazy<ReadOnlyCollection<CarGroundConnectConfig>>(
                    () => ExcelParser.Parser<CarGroundConnectConfig>(appConfigPath).ToList().AsReadOnly());

            CarElveConfigCollection =
                new Lazy<ReadOnlyCollection<CarElveConfig>>(
                    () => ExcelParser.Parser<CarElveConfig>(appConfigPath).ToList().AsReadOnly());

            CarMotoTemperatureConfigCollection =
                new Lazy<ReadOnlyCollection<CarMotoTemperatureConfig>>(
                    () => ExcelParser.Parser<CarMotoTemperatureConfig>(appConfigPath).ToList().AsReadOnly());

            CarTowBrakeKNConfigCollection =
                new Lazy<ReadOnlyCollection<CarTowBrakeKNConfig>>(
                    () => ExcelParser.Parser<CarTowBrakeKNConfig>(appConfigPath).ToList().AsReadOnly());

            CarAirCompreeConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirCompreeConfig>>(
                    () => ExcelParser.Parser<CarAirCompreeConfig>(appConfigPath).ToList().AsReadOnly());

            CarBogieIsolationValveConfigCollection =
                new Lazy<ReadOnlyCollection<CarBogieIsolationValveConfig>>(
                    () => ExcelParser.Parser<CarBogieIsolationValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarEmergBrakeRelayConfigCollection =
                new Lazy<ReadOnlyCollection<CarEmergBrakeRelayConfig>>(
                    () => ExcelParser.Parser<CarEmergBrakeRelayConfig>(appConfigPath).ToList().AsReadOnly());

            CarPackingBrakeIsolationValveConfigCollection =
                new Lazy<ReadOnlyCollection<CarPackingBrakeIsolationValveConfig>>(
                    () => ExcelParser.Parser<CarPackingBrakeIsolationValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarAirSpringPreesureConfigCollection =
                new Lazy<ReadOnlyCollection<CarAirSpringPreesureConfig>>(
                    () => ExcelParser.Parser<CarAirSpringPreesureConfig>(appConfigPath).ToList().AsReadOnly());

            CarBrakePressureConfigCollection =
                new Lazy<ReadOnlyCollection<CarBrakePressureConfig>>(
                    () => ExcelParser.Parser<CarBrakePressureConfig>(appConfigPath).ToList().AsReadOnly());

            CarMainPressureConfigCollection =
                new Lazy<ReadOnlyCollection<CarMainPressureConfig>>(
                    () => ExcelParser.Parser<CarMainPressureConfig>(appConfigPath).ToList().AsReadOnly());

            CarWeightConfigCollection =
                new Lazy<ReadOnlyCollection<CarWeightConfig>>(
                    () => ExcelParser.Parser<CarWeightConfig>(appConfigPath).ToList().AsReadOnly());

            CarLadenConfigCollection =
                new Lazy<ReadOnlyCollection<CarLadenConfig>>(
                    () => ExcelParser.Parser<CarLadenConfig>(appConfigPath).ToList().AsReadOnly());

            StationConfigCollection = new Lazy<ReadOnlyCollection<StationConfig>>(
                () => ExcelParser.Parser<StationConfig>(appConfigPath).ToList().AsReadOnly());

            EmergBroadcastConfigCollection = new Lazy<ReadOnlyCollection<EmergBroadcastConfig>>(
                () => ExcelParser.Parser<EmergBroadcastConfig>(appConfigPath).ToList().AsReadOnly());
            CarPowerSwitchConfigCollection = new Lazy<ReadOnlyCollection<CarPowerSwitchConfig>>(
                            () => ExcelParser.Parser<CarPowerSwitchConfig>(appConfigPath).ToList().AsReadOnly());

            CarBatteryChargerStateConfigCollection = new Lazy<ReadOnlyCollection<CarBatteryChargerStateConfig>>(
                                        () => ExcelParser.Parser<CarBatteryChargerStateConfig>(appConfigPath).ToList().AsReadOnly());
            CarStorageBatterytemperatureConfigCollection = new Lazy<ReadOnlyCollection<CarStorageBatterytemperatureConfig>>(
                                        () => ExcelParser.Parser<CarStorageBatterytemperatureConfig>(appConfigPath).ToList().AsReadOnly());
            CarAssistLoadSwitchConfigCollection = new Lazy<ReadOnlyCollection<CarAssistLoadSwitchConfig>>(
                                        () => ExcelParser.Parser<CarAssistLoadSwitchConfig>(appConfigPath).ToList().AsReadOnly());
            CarStorageBatteryAConfigCollection = new Lazy<ReadOnlyCollection<CarStorageBatteryAConfig>>(
                                        () => ExcelParser.Parser<CarStorageBatteryAConfig>(appConfigPath).ToList().AsReadOnly());

            CarGroup1NewAirValveConfigCollection = new Lazy<ReadOnlyCollection<CarGroup1NewAirValveConfig>>(
                                       () => ExcelParser.Parser<CarGroup1NewAirValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarGroup1BackAirValveConfigCollection = new Lazy<ReadOnlyCollection<CarGroup1BackAirValveConfig>>(
                                                   () => ExcelParser.Parser<CarGroup1BackAirValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarGroup2NewAirValveConfigCollection = new Lazy<ReadOnlyCollection<CarGroup2NewAirValveConfig>>(
                                                   () => ExcelParser.Parser<CarGroup2NewAirValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarGroup2BackAirValveConfigCollection = new Lazy<ReadOnlyCollection<CarGroup2BackAirValveConfig>>(
                                                   () => ExcelParser.Parser<CarGroup2BackAirValveConfig>(appConfigPath).ToList().AsReadOnly());

            CarAlertBypassConfigCollection = new Lazy<ReadOnlyCollection<CarAlertBypassConfig>>(
                                                              () => ExcelParser.Parser<CarAlertBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarMainWindLowPresByPassConfigCollection = new Lazy<ReadOnlyCollection<CarMainWindLowPresByPassConfig>>(
                                                              () => ExcelParser.Parser<CarMainWindLowPresByPassConfig>(appConfigPath).ToList().AsReadOnly());
            CarParkingRelaseBypassConfigCollection = new Lazy<ReadOnlyCollection<CarParkingRelaseBypassConfig>>(
                                                              () => ExcelParser.Parser<CarParkingRelaseBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarNormalBrakeBypassConfigCollection = new Lazy<ReadOnlyCollection<CarNormalBrakeBypassConfig>>(
                                                              () => ExcelParser.Parser<CarNormalBrakeBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarDoorBypassConfigCollection = new Lazy<ReadOnlyCollection<CarDoorBypassConfig>>(
                                                              () => ExcelParser.Parser<CarDoorBypassConfig>(appConfigPath).ToList().AsReadOnly());

            CarCoulplingStateConfigCollection = new Lazy<ReadOnlyCollection<CarCoulplingStateConfig>>(
                                                                         () => ExcelParser.Parser<CarCoulplingStateConfig>(appConfigPath).ToList().AsReadOnly());
            CarPantographEnableBypassConfigCollection = new Lazy<ReadOnlyCollection<CarPantographEnableBypassConfig>>(
                                                              () => ExcelParser.Parser<CarPantographEnableBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarATCCutOffBypassConfigCollection = new Lazy<ReadOnlyCollection<CarATCCutOffBypassConfig>>(
                                                              () => ExcelParser.Parser<CarATCCutOffBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarDoorEnableBypassConfigCollection = new Lazy<ReadOnlyCollection<CarDoorEnableBypassConfig>>(
                                                              () => ExcelParser.Parser<CarDoorEnableBypassConfig>(appConfigPath).ToList().AsReadOnly());
            CarKeyBypassConfigCollection = new Lazy<ReadOnlyCollection<CarKeyBypassConfig>>(
                                                              () => ExcelParser.Parser<CarKeyBypassConfig>(appConfigPath).ToList().AsReadOnly());

            CarAirConditionConfigCollection = new Lazy<ReadOnlyCollection<CarAirConditionConfig>>(
                                                                         () => ExcelParser.Parser<CarAirConditionConfig>(appConfigPath).ToList().AsReadOnly());
            CarTemperatureConfigCollection = new Lazy<ReadOnlyCollection<CarTemperatureConfig>>(
                                                                                    () => ExcelParser.Parser<CarTemperatureConfig>(appConfigPath).ToList().AsReadOnly());
            CarControlModelConfigCollection = new Lazy<ReadOnlyCollection<CarControlModelConfig>>(
                                                                                    () => ExcelParser.Parser<CarControlModelConfig>(appConfigPath).ToList().AsReadOnly());
        }
    }
}