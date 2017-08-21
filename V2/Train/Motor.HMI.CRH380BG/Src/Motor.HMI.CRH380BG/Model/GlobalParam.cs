using System;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.HMI.CRH380BG.Model.ConfigModel;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using System.IO;
using Motor.HMI.CRH380BG.Model.Units;
using Motor.HMI.CRH380BG.Service;

namespace Motor.HMI.CRH380BG.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(CRH380BGSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        public Lazy<ReadOnlyCollection<NotifyInfoConfig>> NotifyInfoConfigs { private set; get; }
        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory,
                    Path.Combine(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, "Motor.HMI.CRH380BG"));
        }

        public void Initalize(string rootConfigPath, string appConfigPath, string btnConfigPath)
        {
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(btnConfigPath).ToList().AsReadOnly();

            NotifyInfoConfigs =
                new Lazy<ReadOnlyCollection<NotifyInfoConfig>>(
                    () => ExcelParser.Parser<NotifyInfoConfig>(rootConfigPath).ToList().AsReadOnly());
        }
    }
}