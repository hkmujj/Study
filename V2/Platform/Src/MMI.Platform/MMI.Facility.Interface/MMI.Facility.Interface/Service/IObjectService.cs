using System.Collections.ObjectModel;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] GetObject<T>(string appName) where T : baseClass;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        ReadOnlyCollection<IObjectBase> GetAllObject(string appName);

    }
}
