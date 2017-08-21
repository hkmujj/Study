using System;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TPX21F.HXN5B.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }
        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<MonitorSelfChargingItemConfig>> MonitorSelfChargingItems { private set; get; }

        public Lazy<ReadOnlyCollection<MonitorOilEnginStartItemConfig>> MonitorOilEnginStartItems { private set; get; }
        public Lazy<ReadOnlyCollection<MonitorDCUItemConfig>> MonitorDCUItemConfigs { private set; get; }
        public Lazy<ReadOnlyCollection<MonitorECUItemConfig>> MonitorECUItemConfigs { private set; get; }

        public Lazy<ReadOnlyCollection<MonitorPhaseControlItemConfig>> MonitorPhaseControlItemConfigs { private set;
            get; }

        public Lazy<ReadOnlyCollection<MonitorAssistMachineItemConfig>> MonitorAssistMachineItemConfigs { private set;
            get; }

        public Lazy<ReadOnlyCollection<MonitorEngineItemConfig>> MonitorEngineItemConfigs { private set; get; }
        public Lazy<ReadOnlyCollection<MonitorTowItemConfig>> MonitorTowItemConfigs { private set; get; }

        public Lazy<ReadOnlyCollection<SoftSwitchItemConfig>> SoftSwitchItemConfigs { private set; get; }

        public Lazy<ReadOnlyCollection<TestSelfConditionItemConfig>> TestSelfConditionItemConfigs { private set; get; }

        public Lazy<ReadOnlyCollection<TestVigilantConditionItemConfig>> TestVigilantConditionItemConfigs { private set;
            get; }

        public Lazy<ReadOnlyCollection<TestSpeedConditionItemConfig>> TestSpeedConditionItemConfigs { private set; get;
        }

        public Lazy<ReadOnlyCollection<NotifyInfoConfig>> NotifyInfoConfigs { private set; get; }

        public Lazy<ReadOnlyCollection<AutoStartEngineItemConfig>> AutoStartConfigs { get; private set; }

        public Lazy<ReadOnlyCollection<OperConsoleSelectItemConfig>> OperConsoleSelectItemConfigs { get; private set; }

        public Lazy<AccumulativeDataConfig> AccumulativeDataConfig { private set; get; }

        public Lazy<ReadOnlyCollection<SelfTestItemConfig>> SelfTestItemConfigs { get; private set; }

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

            OperConsoleSelectItemConfigs = new Lazy<ReadOnlyCollection<OperConsoleSelectItemConfig>>(() =>
                ExcelParser.Parser<OperConsoleSelectItemConfig>(appConfigPath).ToList().AsReadOnly());

            SelfTestItemConfigs =
                new Lazy<ReadOnlyCollection<SelfTestItemConfig>>(
                    () => ExcelParser.Parser<SelfTestItemConfig>(appConfigPath).ToList().AsReadOnly());

            AccumulativeDataConfig =
                new Lazy<AccumulativeDataConfig>(
                    () =>
                        new AccumulativeDataConfig(
                            ExcelParser.Parser<AccumulativePowerConfig>(appConfigPath).ToList().AsReadOnly(),
                            ExcelParser.Parser<AccumulativeTimeConfig>(appConfigPath).ToList().AsReadOnly(),
                            ExcelParser.Parser<AccumulativeTowTimeConfig>(appConfigPath).ToList().AsReadOnly(),
                            ExcelParser.Parser<AccumulativeBrakeTimeConfig>(appConfigPath).ToList().AsReadOnly()));

            AutoStartConfigs =
                new Lazy<ReadOnlyCollection<AutoStartEngineItemConfig>>(
                    () => ExcelParser.Parser<AutoStartEngineItemConfig>(appConfigPath).ToList().AsReadOnly());

            NotifyInfoConfigs =
                new Lazy<ReadOnlyCollection<NotifyInfoConfig>>(
                    () => ExcelParser.Parser<NotifyInfoConfig>(rootConfigPath).ToList().AsReadOnly());

            MonitorSelfChargingItems =
                new Lazy<ReadOnlyCollection<MonitorSelfChargingItemConfig>>(
                    () => ExcelParser.Parser<MonitorSelfChargingItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorOilEnginStartItems =
                new Lazy<ReadOnlyCollection<MonitorOilEnginStartItemConfig>>(
                    () => ExcelParser.Parser<MonitorOilEnginStartItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorDCUItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorDCUItemConfig>>(
                    () => ExcelParser.Parser<MonitorDCUItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorECUItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorECUItemConfig>>(
                    () => ExcelParser.Parser<MonitorECUItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorPhaseControlItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorPhaseControlItemConfig>>(
                    () => ExcelParser.Parser<MonitorPhaseControlItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorAssistMachineItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorAssistMachineItemConfig>>(
                    () => ExcelParser.Parser<MonitorAssistMachineItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorEngineItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorEngineItemConfig>>(
                    () => ExcelParser.Parser<MonitorEngineItemConfig>(appConfigPath).ToList().AsReadOnly());

            MonitorTowItemConfigs =
                new Lazy<ReadOnlyCollection<MonitorTowItemConfig>>(
                    () => ExcelParser.Parser<MonitorTowItemConfig>(appConfigPath).ToList().AsReadOnly());

            SoftSwitchItemConfigs =
                new Lazy<ReadOnlyCollection<SoftSwitchItemConfig>>(
                    () => ExcelParser.Parser<SoftSwitchItemConfig>(appConfigPath).ToList().AsReadOnly());

            TestSelfConditionItemConfigs =
                new Lazy<ReadOnlyCollection<TestSelfConditionItemConfig>>(
                    () => ExcelParser.Parser<TestSelfConditionItemConfig>(appConfigPath).ToList().AsReadOnly());

            TestVigilantConditionItemConfigs =
                new Lazy<ReadOnlyCollection<TestVigilantConditionItemConfig>>(
                    () => ExcelParser.Parser<TestVigilantConditionItemConfig>(appConfigPath).ToList().AsReadOnly());

            TestSpeedConditionItemConfigs =
                new Lazy<ReadOnlyCollection<TestSpeedConditionItemConfig>>(
                    () => ExcelParser.Parser<TestSpeedConditionItemConfig>(appConfigPath).ToList().AsReadOnly());

        }
    }
}