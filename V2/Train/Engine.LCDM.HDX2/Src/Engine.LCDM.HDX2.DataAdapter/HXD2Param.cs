using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.DataAdapter
{
    public class HXD2Param
    {
        public static readonly HXD2Param Instance = new HXD2Param();
        private IProjectIndexDescriptionConfig m_IndexDescriptionConfig;

        private HXD2Param()
        {

        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
        }

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescriptionConfig
        {
            get
            {
                return m_IndexDescriptionConfig ??
                       (m_IndexDescriptionConfig =
                           InitParam.DataPackage.Config.IndexDescriptionConfig
                               .GetProjectIndexDescriptionConfig(
                                   new CommunicationDataKey(Instance.InitParam.AppConfig)));
            }
        }
    }
}