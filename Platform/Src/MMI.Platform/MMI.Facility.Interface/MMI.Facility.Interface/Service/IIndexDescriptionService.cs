using System.Collections.Generic;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    public interface IIndexDescriptionService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IIndexInterpreter> AllIndexInterpreters { get; }

            /// <summary>
        /// 
        /// </summary>
        /// <param name="communicationDataKey"></param>
        /// <returns></returns>
        IIndexInterpreter GetIndexInterpreter(ICommunicationDataKey communicationDataKey);
    }
}