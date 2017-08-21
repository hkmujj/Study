using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Motor.ATP._200H.ConfigModel;

namespace Motor.ATP._200H.Common
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GlobalParam : baseClass
    {
        private ATP200HBrakeConfig m_Atp200HBrakeConfig;

        public ATP200HBrakeConfig Atp200HBrakeConfig
        {
            get
            {
                if (m_Atp200HBrakeConfig == null)
                {
                    LoadBrakeConfig();
                }
                return m_Atp200HBrakeConfig;
            }
            set { m_Atp200HBrakeConfig = value; }
        }

        private IProjectIndexDescriptionConfig m_IndexDescriptionConfig;

        public ATP200HDetailConfig DetailConfig { private set; get; }

        public static GlobalParam Instance { private set; get; }

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;

            m_IndexDescriptionConfig =
                IConfig.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(new CommunicationDataKey(AppConfig));

            DetailConfig =
                DataSerialization.DeserializeFromXmlFile<ATP200HDetailConfig>(Path.Combine(AppPaths.ConfigDirectory,
                    ATP200HDetailConfig.FileName));
            if (DetailConfig.AdapterConfig == null)
            {
                DetailConfig.AdapterConfig = new AdapterConfig();
            }

            return true;
        }

        public int FindInBoolIndex(string name)
        {
            if (m_IndexDescriptionConfig.InBoolDescriptionDictionary.ContainsKey(name))
            {
                return m_IndexDescriptionConfig.InBoolDescriptionDictionary[name];
            }
            return int.MaxValue;
        }

        private void LoadBrakeConfig()
        {
            var configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            Atp200HBrakeConfig =
                DataSerialization.DeserializeFromXmlFile<ATP200HBrakeConfig>(Path.Combine(configDir,
                    ATP200HBrakeConfig.FileName));
        }
    }
}