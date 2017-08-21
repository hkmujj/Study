using System;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class EventService : IEventService
    {
        public event Action<CourseStartEventArgs> CoursStarting;

        public event Action<EventArgs> CourseStopping;

        public void OnCourseStopping(EventArgs args = null)
        {
            SysLog.Debug("Raise course stop envent.");
            var handler = CourseStopping;
            if (handler != null)
            {
                handler(args);
            }
        }

        public void OnCoursStarting(CourseStartEventArgs obj)
        {
            SysLog.Debug("Raise course start envent.");
            var handler = CoursStarting;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}