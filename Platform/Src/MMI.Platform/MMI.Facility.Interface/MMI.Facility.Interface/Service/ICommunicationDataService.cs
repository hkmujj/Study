using System;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationDataService : IDisposable
    {
        /// <summary>
        /// 测试时才能用
        /// </summary>
        IWritableCommunicationDataReadService WritableReadService { get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataReadService ReadService { get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataWriteService WriteService { get; }

    }
}
