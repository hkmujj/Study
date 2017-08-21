using System;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public StartType StartType { private set; get; }

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }
        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<TrainInfoPage2Config>> TrainInfoPage2ConfigCollection { private set; get; }

        public Lazy<ReadOnlyCollection<SetMonitorItemConfig>> SetMonitorItemConfigs { get; private set; }

        public Lazy<ReadOnlyCollection<SetAccDataItemConfig>> SetAccDataItemConfigCollection { get; private set; }

        public void Initalize(SubsystemInitParam initParam, StartType startType = StartType.MMI)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory, startType);
        }

        public void Initalize(string rootConfigPath, string appConfigPath, StartType startType = StartType.Bootstrapper)
        {
            StartType = startType;

            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();
            TrainInfoPage2ConfigCollection =
                new Lazy<ReadOnlyCollection<TrainInfoPage2Config>>(
                    () => ExcelParser.Parser<TrainInfoPage2Config>(appConfigPath).ToList().AsReadOnly());

            SetMonitorItemConfigs =
                new Lazy<ReadOnlyCollection<SetMonitorItemConfig>>(
                    () => ExcelParser.Parser<SetMonitorItemConfig>(appConfigPath).ToList().AsReadOnly());

            SetAccDataItemConfigCollection =
                new Lazy<ReadOnlyCollection<SetAccDataItemConfig>>(
                    () => ExcelParser.Parser<SetAccDataItemConfig>(appConfigPath).ToList().AsReadOnly());
        }
    }
}