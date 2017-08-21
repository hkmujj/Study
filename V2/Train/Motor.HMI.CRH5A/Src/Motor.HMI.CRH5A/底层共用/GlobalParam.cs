using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using Motor.HMI.CRH5A.Config.ConfigModel;

namespace Motor.HMI.CRH5A.底层共用
{
    public class GlobalParam
    {
        private ProjectConfig m_ProjectConfig;
        private Dictionary<CommunicationDataType, DataSet> m_CommunicationDataIndexDictionary;
        private ProjectDetailConfig m_ProjectDetailConfig;

        public static GlobalParam Instance { private set; get; }

        public Dictionary<CommunicationDataType, DataSet> CommunicationDataIndexDictionary
        {
            get { return m_CommunicationDataIndexDictionary ?? (m_CommunicationDataIndexDictionary = LoadCommunicationDataIndexConfig()); }
        }

        public ProjectDetailConfig ProjectDetailConfig
        {
            get { return m_ProjectDetailConfig ?? (m_ProjectDetailConfig = LoadDetailConfig()); }
        }

        public int FindInBoolIndex(string name)
        {
            return Convert.ToInt32(m_CommunicationDataIndexDictionary[CommunicationDataType.InBool].Tables[0].Rows.Find(name)[1]);
        }
        public int FindOutBoolIndex(string name)
        {
            return Convert.ToInt32(m_CommunicationDataIndexDictionary[CommunicationDataType.OutBool].Tables[0].Rows.Find(name)[1]);
        }
        public int FindInFloatIndex(string name)
        {
            return Convert.ToInt32(m_CommunicationDataIndexDictionary[CommunicationDataType.InFloat].Tables[0].Rows.Find(name)[1]);
        }
        public int FindOutFloatIndex(string name)
        {
            return Convert.ToInt32(m_CommunicationDataIndexDictionary[CommunicationDataType.OutFloat].Tables[0].Rows.Find(name)[1]);
        }
        public ProjectConfig ProjectConfig
        {
            get
            {
                if (m_ProjectConfig == null)
                {
                    LoadProjectConfig();
                }
                return m_ProjectConfig;
            }
        }

        static GlobalParam()
        {
            Instance = new GlobalParam();
        }

        private void LoadProjectConfig()
        {
            m_ProjectConfig = DataSerialization.DeserializeFromXmlFile<ProjectConfig>(ProjectConfig.File);
            m_ProjectConfig.Correct(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"));
        }


        private Dictionary<CommunicationDataType, DataSet> LoadCommunicationDataIndexConfig()
        {
            var dic = new Dictionary<CommunicationDataType, DataSet>();
            foreach (var config in ProjectConfig.CRH5AFileConfig.CRH5ACommunicationConfigCollection)
            {
                if (dic.ContainsKey(config.DataType))
                {
                    LogMgr.Warn(string.Format("Communication config has more than one node of {0}", config.DataType));
                }
                else
                {
                    dic.Add(config.DataType, config.Adapter());
                }
            }

            if (dic.Keys.Count != Enum.GetNames(typeof(CommunicationDataType)).Length)
            {
                LogMgr.Warn("There is not enough communicaion index config .");
            }

            return dic;
        }

        private ProjectDetailConfig LoadDetailConfig()
        {
            if (!File.Exists(ProjectConfig.DetailConfigFile))
            {
                LogMgr.Fatal(string.Format("Can not found project detail config file {0}.", ProjectConfig.DetailConfigFile));
                return null;
            }

            return DataSerialization.DeserializeFromXmlFile<ProjectDetailConfig>(ProjectConfig.DetailConfigFile);
        }
    }
}