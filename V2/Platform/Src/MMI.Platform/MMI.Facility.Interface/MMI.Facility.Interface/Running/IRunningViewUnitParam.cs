using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Running
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunningViewUnitParam
    {
        /// <summary>
        /// 
        /// </summary>
        IAppViewConfigUnit ViewConfig { get; }

        /// <summary>
        /// 此视图下的对象 
        /// </summary>
        List<IObjectBase> Objects { get; }
    }
}
