using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 数据变化服务
    /// </summary>
    public interface IDataChangeListenService : IService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="appConfig"></param>
        void RegistListener(IDataListener listener, IAppConfig appConfig);

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="appConfig"></param>
        void UnregistListener(IDataListener listener, IAppConfig appConfig);

    }


    /// <summary>
    /// 数据监听
    /// </summary>
    public interface IDataListener
    {
        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs);

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs);

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs);
    }
}