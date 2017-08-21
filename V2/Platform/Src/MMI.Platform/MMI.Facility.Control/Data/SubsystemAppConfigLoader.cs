using System;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Control.Extension;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Form;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Control.Data
{
    internal class SubsystemAppConfigLoader : IAppConfigLoader
    {
        public virtual IAppConfig LoadAppConfig(IConfig rootConfig, string baseDirectory,
            ISubsystemConfig subsystemConfig)
        {
            var config = new AppConfig();
            try
            {
                LoadActureFormConfig(baseDirectory, config, subsystemConfig.Name);
            }
            catch (Exception e)
            {
                var msg = string.Format("Load app ¡¾{0}¡¿ config  error. {1}", subsystemConfig.Name, e);
                LogMgr.Fatal(msg);
                throw new Exception(msg);
            }

            LogMgr.Info(string.Format("Load app  ¡¾{0}¡¿ config {0} sucess.", subsystemConfig.Name));

            var projectConfig =
                rootConfig.SystemConfig.SubsystemConfigCollection.First(f => f.Name == subsystemConfig.Name);
            config.AppName = subsystemConfig.Name;
            config.SubsystemConfig = projectConfig;
            config.ParentConfig = rootConfig;
            config.AppPaths = new AppPaths(baseDirectory, config);

            var file = Path.Combine(config.AppPaths.ConfigDirectory, AppCommunicationInterfaceConfig.ConfigFileName);
            if (File.Exists(file))
            {
                config.AppCommunicationInterfaceConfig =
                    DataSerialization.DeserializeFromXmlFile<AppCommunicationInterfaceConfig>(file);
                if (config.AppCommunicationInterfaceConfig != null)
                {
                    config.AppCommunicationInterfaceConfig.InterfaceModelCollection.ForEach(
                        e => e.LoadInterfaceFile(config.AppPaths.ConfigDirectory));
                }
            }
            else
            {
                SysLog.Warn("Can not found AppCommunicationInterfaceConfig , {0}", file);
            }
            return config;
        }

        private void LoadActureFormConfig(string baseDirectory, AppConfig appConfig, string appName)
        {
            var configDicrectory = Path.Combine(baseDirectory, string.Format("{0}\\Config", appName));

            var formConfig =
                DataSerialization.DeserializeFromXmlFile<ActureFormConfig>(Path.Combine(configDicrectory,
                    ActureFormConfig.FileName));

            appConfig.ActureFormConfig = formConfig;
        }
    }
}