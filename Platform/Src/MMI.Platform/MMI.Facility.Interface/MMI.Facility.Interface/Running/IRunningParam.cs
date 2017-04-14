using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Running
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunningParam
    {
        /// <summary>
        /// 
        /// </summary>
        StartModel StartModel { get; }

        /// <summary>
        /// key : 工程名
        /// </summary>
        Dictionary<string, IAppRunningParam> AppRunningParamDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataFacadeService CommunicationFacadeDataService { get; }


    }
}
