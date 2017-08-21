using CommonUtil.Util;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Subway.XiaMenLine1.Subsystem
{
    public class SubsysParams
    {
        public static readonly SubsysParams Instance = new SubsysParams();

        public void Initalize(SubsystemInitParam initParam)
        {
            SubsystemInitParam = initParam;

            IndexDescriptionConfig =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
            if (IndexDescriptionConfig == null)
            {
                AppLog.Error("Can not found IndexDescriptionConfig where app = {0}", initParam.AppConfig.AppName);
            }

        }
        
        public SubsystemInitParam SubsystemInitParam { private set; get; }


        public IProjectIndexDescriptionConfig IndexDescriptionConfig { private set; get; }
    }
}