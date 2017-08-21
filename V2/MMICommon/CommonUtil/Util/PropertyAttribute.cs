using System;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PropertyAttribute()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PropertyAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
