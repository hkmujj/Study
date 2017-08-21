using System;

namespace Subway.ShenZhenLine9.CusstomAttribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ParentAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">类型</param>
        public ParentAttribute(Type type)
        {
            Type = type;
        }
        /// <summary>
        /// 控件类型
        /// </summary>
        public Type Type { get; private set; }
    }
}