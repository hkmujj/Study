using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// APP通信数据接口配置
    /// </summary>
    public interface IAppCommunicationInterfaceConfig
    {
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 接口模型
        /// </summary>
        List<IAppCommunicationInterfaceModel> InterfaceModelCollection { get; }
    }
}