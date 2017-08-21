using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Subway.ShenZhenLine11.Config
{
    public class GlobalParam
    {
        public static GlobalParam Instance { get; private set; }

        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
        public SubsystemInitParam InitPara { get; internal set; }
        private IProjectIndexDescriptionConfig m_IndexConfig;
        public IProjectIndexDescriptionConfig IndexConfig
        {
            get
            {
                if (m_IndexConfig == null)
                {
                    m_IndexConfig =
                        InitPara.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                            new CommunicationDataKey(InitPara.AppConfig));
                }
                return m_IndexConfig;
            }

        }

    }
}