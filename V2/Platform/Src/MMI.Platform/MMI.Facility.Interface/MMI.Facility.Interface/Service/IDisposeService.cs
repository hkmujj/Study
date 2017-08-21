using System;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    ///  IDisposable 的清除服务
    /// </summary>
    public interface IDisposeService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposable"></param>
        void RegistDisposableObject(IDisposable disposable);
    }
}