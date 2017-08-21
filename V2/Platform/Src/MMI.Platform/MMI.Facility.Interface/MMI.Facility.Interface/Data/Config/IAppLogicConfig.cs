using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppLogicConfig
    {
        /// <summary>
        /// 
        /// </summary>
        IAppConfig ParentConfig { get; }

        /// <summary>
        /// 编号
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 资源路径
        /// </summary>
        string RecPath { get; }

        /// <summary>
        /// 
        /// </summary>
        List<int> LeftDataList { get; }

        /// <summary>
        /// 
        /// </summary>
        List<int> RightDataList { get; }

        /// <summary>
        /// 
        /// </summary>
        LogicType LogicRunType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Enable { get; }

        /// <summary>
        /// 
        /// </summary>
        string Memo { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAppLogicConfigCollection
    {
        /// <summary>
        /// 
        /// </summary>
        IAppConfig ParentConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IAppLogicConfig> AppLogicConfigDic { get; }
    }
}
