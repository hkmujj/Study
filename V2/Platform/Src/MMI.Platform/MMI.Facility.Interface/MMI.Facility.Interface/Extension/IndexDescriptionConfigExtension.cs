using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class IndexDescriptionConfigExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectType"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static IProjectIndexDescriptionConfig GetProjectIndexDescriptionConfig(this IIndexDescriptionConfig config,
            ProjectType projectType, string appName)
        {
            return config.GetProjectIndexDescriptionConfig(new CommunicationDataKey(projectType, appName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="appConfig"></param>
        /// <returns></returns>
        public static IProjectIndexDescriptionConfig GetProjectIndexDescriptionConfig(
            this IIndexDescriptionConfig config,
            IAppConfig appConfig)
        {
            return config.GetProjectIndexDescriptionConfig(appConfig.ProjectType, appConfig.AppName);
        }
    }
}