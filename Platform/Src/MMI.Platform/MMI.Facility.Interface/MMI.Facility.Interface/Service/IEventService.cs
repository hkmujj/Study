using System;
using MMI.Facility.Interface.Event;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        event Action<CourseStartEventArgs> CoursStarting;

        /// <summary>
        /// 
        /// </summary>
        event Action<EventArgs> CourseStopping;
    }
}