using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 所有的屏的关联关系
    /// </summary>
    public interface IScreenTableConfig
    {
        /// <summary>
        /// 屏元素表。
        /// </summary>
        List<IScreenItem> ScreenItems { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IScreenItem
    {
        /// <summary>
        /// 关键字，主控发送
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        List<string> ProjectCollection { get; }
    }
}