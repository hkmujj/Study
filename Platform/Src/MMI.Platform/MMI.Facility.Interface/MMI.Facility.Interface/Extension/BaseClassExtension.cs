using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// BaseClass扩展
    /// </summary>
    public static class BaseClassExtension
    {
        /// <summary>
        /// 注册数据监听
        /// </summary>
        /// <param name="baseClass"></param>
        /// <param name="listener"></param>
        public static void RegistDataListener(this baseClass baseClass, IDataListener listener)
        {
            baseClass.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(listener, baseClass.AppConfig);
        }

        /// <summary>
        /// 取消注册数据监听
        /// </summary>
        /// <param name="baseClass"></param>
        /// <param name="listener"></param>
        public static void UnregistDataListener(this baseClass baseClass, IDataListener listener)
        {
            baseClass.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .UnregistListener(listener, baseClass.AppConfig);
        }
    }
}