using System.Threading;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Course;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.View.ViewModel
{
    public class CourseViewModel : NotificationObject
    {
        private CourseStartParameter m_CourseStartParameter;

        private CourseState m_CurrentCourseState;
        private IDataPackage m_DataPackage;

        public IDataPackage DataPackage
        {
            set
            {
                m_DataPackage = value;
                CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
                CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
            }
            get { return m_DataPackage; }
        }

        public ICourseService CourseService { get; private set; }

        public CourseState CurrentCourseState
        {
            set
            {
                if (value == m_CurrentCourseState)
                {
                    return;
                }

                RaiseCourseChanged(value);

                m_CurrentCourseState = value;
                RaisePropertyChanged(() => CurrentCourseState);
            }
            get { return m_CurrentCourseState; }
        }

        public CourseStartParameter CourseStartParameter
        {
            set
            {
                if (Equals(value, m_CourseStartParameter))
                {
                    return;
                }

                m_CourseStartParameter = value;
                RaisePropertyChanged(() => CourseStartParameter);
            }
            get { return m_CourseStartParameter; }
        }

        public CourseViewModel()
        {
            CurrentCourseState = CourseState.Unknown;
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (sender != this)
            {
                CurrentCourseState = courseStateChangedArgs.CourseService.CurrentCourseState;
            }
        }

        private void RaiseCourseChanged(CourseState value)
        {
            if (CourseService != null)
            {
                // 模拟接收数据线程
                ThreadPool.QueueUserWorkItem(state => CourseService.ChangeState(value, null, this));
            }
        }
    }
}