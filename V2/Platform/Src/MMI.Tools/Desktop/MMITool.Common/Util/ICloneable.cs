using System;

namespace MMITool.Common.Util
{
    /// <summary>
    /// ICloneable 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// clone , 返回具体类型
        /// </summary>
        /// <returns></returns>
        new T Clone();
    }
}
