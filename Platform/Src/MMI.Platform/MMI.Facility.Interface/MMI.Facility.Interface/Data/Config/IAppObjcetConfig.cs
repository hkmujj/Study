using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 对象配置表
    /// </summary>
    public interface IAppObjcetConfig
    {

        /// <summary>
        /// 
        /// </summary>
        IAppConfig ParentConfig { get; }

        /// <summary>
        /// 文件名
        /// </summary>
        string FileName {  get; }

        /// <summary>
        /// 解析后得到的
        /// </summary>
        List<IUIObject> UIObjects { get; }

        /// <summary>
        /// 原始信息
        /// </summary>
        List<IAppBaseObjectInfo> AppBaseObjectInfo { get; }
    }
}
