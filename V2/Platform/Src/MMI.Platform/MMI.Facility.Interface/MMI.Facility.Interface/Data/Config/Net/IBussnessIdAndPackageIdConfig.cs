using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 业务id和包号的配置
    /// </summary>
    public interface IBussnessIdAndPackageIdConfig
    {
        /// <summary>
        /// ProjectDataConfigCollection
        /// </summary>
        List<INetProjectDataConfig> ProjectDataConfigCollection { get; }
    }
}