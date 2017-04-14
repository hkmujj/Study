using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// SubsystemInitParam 扩展
    /// </summary>
    public static class SubsystemInitParamExtension
    {
        /// <summary>
        /// 注册数据监听
        /// </summary>
        /// <param name="initParam"></param>
        /// <param name="listener"></param>
        public static void RegistDataListener(this SubsystemInitParam initParam, IDataListener listener)
        {
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(listener, initParam.AppConfig);
        }

        /// <summary>
        /// 取消注册数据监听
        /// </summary>
        /// <param name="initParam"></param>
        /// <param name="listener"></param>
        public static void UnregistDataListener(this SubsystemInitParam initParam, IDataListener listener)
        {
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .UnregistListener(listener, initParam.AppConfig);
        }
    }
}