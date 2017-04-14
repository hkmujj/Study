using System;

namespace MMITool.Common.Util
{
    /// <summary>
    /// ClassAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ClassAttribute : Attribute
    {
        /// <summary>
        /// ClassAttribute
        /// </summary>
        public ClassAttribute()
        { 
        }

        /// <summary>
        /// ClassAttribute
        /// </summary>
        /// <param name="name"></param>
        public ClassAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
