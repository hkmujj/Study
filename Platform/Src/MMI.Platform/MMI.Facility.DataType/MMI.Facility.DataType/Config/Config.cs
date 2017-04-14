using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config
{
    public class Config : IConfig
    {
        public ISystemDicrectory SystemDicrectory { get; private set; }

        public IndexDescriptionConfig IndexDescriptionConfig { get; set; }

        IIndexDescriptionConfig IConfig.IndexDescriptionConfig
        {
            get { return IndexDescriptionConfig; }
        }

        public ISystemConfig SystemConfig { get; set; }

        /// <summary>
        /// 所有的屏的关联关系
        /// </summary>
        public ScreenTableConfig ScreenTableConfig { set; get; }

        /// <summary>
        /// 所有的屏的关联关系
        /// </summary>
        IScreenTableConfig IConfig.ScreenTableConfig
        {
            get { return ScreenTableConfig; }
        }

        public INetConfig NetConfig { get; set; }

        public INetDataConfig NetDataConfig { set; get; }

        public IDebugWindowConfig DebugWindowConfig { get; set; }

        public ReadOnlyCollection<IAppConfig> AppConfigs
        {
            get { return m_AllAppConfigs.AsReadOnly(); }
        }

        private List<IAppConfig> m_AllAppConfigs;

        public List<IAppConfig> AllAppConfigs
        {
            get { return m_AllAppConfigs; }
            set { m_AllAppConfigs = value; }
        }

        public Config()
        {
            SystemDicrectory = new SystemDicrectory();
            //SystemConfig = new SystemConfig();
            m_AllAppConfigs = new List<IAppConfig>();
        }
    }
}
