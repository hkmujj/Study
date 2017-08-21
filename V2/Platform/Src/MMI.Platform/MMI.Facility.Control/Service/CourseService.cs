using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class CourseService : ICourseService
    {
        public CourseState CurrentCourseState { get; private set; }

        public event EventHandler<CourseStateChangedArgs> CourseStateChanged;

        private readonly CourseStateChangedEvent m_CourseStateChangedEvent;

        public CourseService()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_CourseStateChangedEvent = eventAggregator.GetEvent<CourseStateChangedEvent>();
            CurrentCourseState = CourseState.Unknown;
            var eventService = ServiceManager.Instance.GetService<IEventService>();
            eventService.CoursStarting += args => ChangeState(CourseState.Started, args);
            eventService.CourseStopping += args => ChangeState(CourseState.Stoped, args);
        }

        public void ChangeState(CourseState state, object param = null, object sender = null)
        {
            var old = CurrentCourseState;
            CurrentCourseState = state;
            OnCourseStateChanged(sender ?? this, new CourseStateChangedArgs(this, old, param));
        }


        protected void OnCourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            SysLog.Info("course state changed , old={0}, current={1}", e.OldCourseState, e.CourseService);
            var handler = CourseStateChanged;
            if (handler != null)
            {
                handler(sender, e);
            }

            m_CourseStateChangedEvent.Publish(e);
        }
    }
}