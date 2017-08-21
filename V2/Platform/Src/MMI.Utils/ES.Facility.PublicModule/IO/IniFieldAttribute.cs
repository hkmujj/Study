using System;
using System.Diagnostics;
using System.Reflection;

namespace ES.Facility.PublicModule.IO
{

    [DebuggerDisplay("MemberType={MemberType} Section={Section} KeyName={KeyName}")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class IniFieldAttribute : Attribute
    {

        public enum Type
        {
            /// <summary>
            /// TODO 
            /// </summary>
            Root,
            Section,
            KeyValue,
        }

        public IniFieldAttribute()
        {
            MemberType = Type.KeyValue;
        }

        /// <summary>
        /// 字段类型， 默认为 KeyValue
        /// </summary>
        public Type MemberType { set; get; }

        /// <summary>
        /// 分区名
        /// </summary>
        public string Section { set; get; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string KeyName { set; get; }

        /// <summary>
        /// 默认值 
        /// </summary>
        public string DefaultValue { set; get; }

        /// <summary>
        /// 实例类型
        /// </summary>
        public System.Type InstanceType { set; get; }


        internal PropertyInfo PropertyInfo { set; get; }
    }
}
