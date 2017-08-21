using System.Collections.Generic;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDebugWindowConfig
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IUserDebugWindownConfig> UserDebugWindownConfigCollection {  get; }
    }
}