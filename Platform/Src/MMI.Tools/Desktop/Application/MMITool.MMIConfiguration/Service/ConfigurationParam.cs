using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Config.ConfigModel;
using MMITool.Common.Util;

namespace MMITool.Addin.MMIConfiguration.Service
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigurationParam
    {
        private readonly string m_RawConfigFile;

        private readonly string m_TempConfigFile;


        [ImportingConstructor]
        public ConfigurationParam(IApplicationService appService)
        {
            m_RawConfigFile = Path.Combine(appService.AddinPath, "Config\\ConfigurationConfig.xml");
            m_TempConfigFile = Path.Combine(appService.AddinPath, "Config\\ConfigurationConfig.Temp.xml");

            ConfigurationConfig = DataSerialization.DeserializeFromXmlFile<ConfigurationConfig>(File.Exists(m_TempConfigFile) ? m_TempConfigFile : m_RawConfigFile);

            if (ConfigurationConfig != null)
            {
                ConfigurationConfig.UpdateFullPath(appService.AddinConfigPath);
            }
        }

        public void SaveSelectableItems(IEnumerable<string> selectable)
        {
            var cf = new ConfigurationConfig(ConfigurationConfig);

            cf.SelectableList.AddRange(selectable);

            SaveConfig(cf);
        }

        private void SaveConfig(ConfigurationConfig configurationConfig)
        {
            DataSerialization.SerializeToXmlFile(configurationConfig, m_TempConfigFile);
        }

        public ConfigurationConfig ConfigurationConfig { private set; get; }
    }
}