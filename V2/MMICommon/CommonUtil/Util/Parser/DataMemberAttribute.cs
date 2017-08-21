using System;
using System.Reflection;

namespace CommonUtil.Util.Parser
{
    /// <summary>
    ///  可以只设定 IsRequire Order,
    /// 同一个类中, Order 必须唯一
    /// </summary>
    public class DataMemberAttribute :Attribute
    {
        /// <summary>
        /// 是否需要解析
        /// </summary>
        public bool IsRequire { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { internal set; get; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Order { set; get; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string PropertyName { internal set; get; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public Type PropertyType { internal set; get; }

        /// <summary>
        /// 字段信息
        /// </summary>
        public PropertyInfo PropertyInfo { internal set; get; }
    }
}
