using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDNetConfig
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<INetProjectDataConfig> ProjectDataConfigCollection { get; }
    }
}