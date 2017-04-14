using System;
using System.Diagnostics;

namespace MMI.Facility.Interface.Event
{
    /// <summary>
    /// 
    /// </summary>
    public class CourseStartEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startParam"></param>
        [DebuggerStepThrough]
        public CourseStartEventArgs(object startParam)
        {
            StartParam = startParam;
        }

        /// <summary>
        /// 
        /// </summary>
        public object StartParam { private set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CourseStartEventArgs<T> : CourseStartEventArgs where T : class 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startParam"></param>
        [DebuggerStepThrough]
        public CourseStartEventArgs(object startParam) : base(startParam)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public new T StartParam { get { return base.StartParam as T; } }
    }
}