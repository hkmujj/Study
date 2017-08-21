using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppViewConfig
    {
        /// <summary>
        /// 
        /// </summary>
        IAppConfig ParentConfig { get; }

        
        /// <summary>
        /// key 索引
        /// </summary>
        Dictionary<int, IAppViewConfigUnit> AppViewConfigDic { get; }
        ///// <summary>
        ///// 存储该视图需要显示的对象集合
        ///// </summary>
        //List<string> ObjectList { get; }
    }
}
