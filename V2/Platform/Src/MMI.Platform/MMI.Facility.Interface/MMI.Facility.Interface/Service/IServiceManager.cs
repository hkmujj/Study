
namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// 获取一个服务， 关键字为 key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>() where T : IService;

        /// <summary>
        /// 注册一个服务， 关键字为 key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool RegistService<T>(object service) where T : IService;

        /// <summary>
        /// 注销一个服务， 关键字为 key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void UnregistService<T>() where T : IService;

        /// <summary>
        /// 获取一个服务， 关键字为 typeof(T).FullName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>(string key) where T : IService;

        /// <summary>
        /// 注册一个服务， 关键字为 key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool RegistService<T>(string key, object service) where T : IService;

        /// <summary>
        /// 注销一个服务， 关键字为 key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void UnregistService<T>(string key) where T : IService;
    }
}