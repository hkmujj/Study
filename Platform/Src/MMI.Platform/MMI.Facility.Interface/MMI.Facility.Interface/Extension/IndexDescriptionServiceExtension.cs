using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class IndexDescriptionServiceExtension
    {
        /// <summary>
        /// GetIndexInterpreter
        /// </summary>
        /// <param name="service"></param>
        /// <param name="projectType"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static IIndexInterpreter GetIndexInterpreter(this IIndexDescriptionService service,
            ProjectType projectType, string appName)
        {
            return service.GetIndexInterpreter(new CommunicationDataKey(projectType, appName));
        }
    }
}