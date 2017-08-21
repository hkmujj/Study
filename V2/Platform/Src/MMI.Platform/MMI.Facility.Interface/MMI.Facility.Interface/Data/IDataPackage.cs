using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataPackage
    {
        /// <summary>
        /// 
        /// </summary>
        IConfig Config { get; }

        /// <summary>
        /// 
        /// </summary>
        IRunningParam RunningParam { get; }

        /// <summary>
        /// 
        /// </summary>
        IAddInLoader AddInLoader { get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataFacadeService CommunicationDataFacadeService { get; }

        /// <summary>
        /// 
        /// </summary>
        IObjectService ObjectService { get; }

        /// <summary>
        /// 
        /// </summary>
        IServiceManager ServiceManager { get; }

    }
}
