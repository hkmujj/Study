using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Running
{
    /// <summary>
    /// 和app有关的运行时参数
    /// </summary>
    public interface IAppRunningParam
    {
        /// <summary>
        /// 
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 
        /// </summary>
        IAppConfig AppConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        IRunningParam ParentParam { get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataService CommunicationDataService { get; }

        /// <summary>
        /// 
        /// </summary>
        IRunningViewParam RunningViewParam { get; }

        /// <summary>
        /// 
        /// </summary>
        IAppPostCmdService AppPostCmdService { get; }

        /// <summary>
        /// 
        /// </summary>
        IRunningLogicCaculate RunningLogicCaculate { get; }

        /// <summary>
        /// 
        /// </summary>
        IIndexInterpreter IndexInterpreter { get; }
    }
}
