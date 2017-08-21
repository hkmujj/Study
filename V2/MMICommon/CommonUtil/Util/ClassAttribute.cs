using System;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ClassAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ClassAttribute()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ClassAttribute(string name)
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
