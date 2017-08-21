using System.Diagnostics;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class SubsystemInitParam
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="appConfig"></param>
        [DebuggerStepThrough]
        public SubsystemInitParam(IDataPackage dataPackage, IAppConfig appConfig)
        {
            DataPackage = dataPackage;
            AppConfig = appConfig;
            CommunicationDataService =
                dataPackage.CommunicationDataFacadeService.GetCommunicationDataService(new CommunicationDataKey(appConfig));
        }

        /// <summary>
        /// 
        /// </summary>
        public IDataPackage DataPackage { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IAppConfig AppConfig { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public ICommunicationDataService CommunicationDataService { private set; get; }

    }
}