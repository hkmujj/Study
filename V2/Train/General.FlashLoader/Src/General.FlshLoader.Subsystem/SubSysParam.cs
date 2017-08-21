using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace General.FlashLoader.Subsystem
{
    internal class SubSysParam
    {
        public static readonly SubSysParam Instance = new SubSysParam();

        public SubsystemInitParam InitParam { get; private set; }

        public IProjectIndexDescriptionConfig IndexDescriptionConfig { private set; get; }

        private SubSysParam()
        {
            
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescriptionConfig =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
        }
    }
}
