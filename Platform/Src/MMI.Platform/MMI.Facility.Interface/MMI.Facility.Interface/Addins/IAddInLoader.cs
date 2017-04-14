using System;
using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Addins
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAddInLoader
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<string, Type> AddinTypeDictionary { get; }
            
            
            /// <summary>
        /// 工程下的插件实例
        /// key : 工程名   key : 查找类型的索引,文件名+类名
        /// </summary>
        Dictionary<string, Dictionary<string, IObjectBase>> ProjetAddinInstanceDic { get; }

        /// <summary>
        /// 加载所有的文件
        /// </summary>
        /// <param name="config"></param>
        void LoadAddin(IConfig config);
    }
}
