using System.IO;
using CommonUtil.Util;
using MMI.Facility.Interface.Project;

namespace Engine.HMI.SS3B.View.Config
{
    public class GlobalParam
    {
        private SS3BConfig m_SS3BConfig;

        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
        public static GlobalParam Instance { get; private set; }
        public SubsystemInitParam InitParam { get; set; }

        public SS3BConfig SS3BConfig
        {
            get
            {
                return m_SS3BConfig ?? (m_SS3BConfig = DataSerialization.DeserializeFromXmlFile<SS3BConfig>(Path.Combine(InitParam.AppConfig.AppPaths.ConfigDirectory, SS3BConfig.FileName)));
            }
            private set
            {
                m_SS3BConfig = value;
            }
        }
    }
}