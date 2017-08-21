using System;

namespace Excel.Interface
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelFieldAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="isPrimaryKey"></param>
        public ExcelFieldAttribute(string field, bool isPrimaryKey = false)
        {
            Field = field;
            IsPrimaryKey = isPrimaryKey;
        }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { private set; get; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { private set; get; }
    }
}