using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommunicationDataFacadeServiceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="facadeService"></param>
        /// <param name="appConfig"></param>
        /// <returns></returns>
        public static ICommunicationDataService GetCommunicationDataService(this ICommunicationDataFacadeService facadeService,
            IAppConfig appConfig)
        {
            return facadeService.GetCommunicationDataService(new CommunicationDataKey(appConfig));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facadeService"></param>
        /// <param name="projectType"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static ICommunicationDataService GetCommunicationDataService(this ICommunicationDataFacadeService facadeService,
            ProjectType projectType, string appName)
        {
            return facadeService.GetCommunicationDataService(new CommunicationDataKey(projectType, appName));
        }

    }
}