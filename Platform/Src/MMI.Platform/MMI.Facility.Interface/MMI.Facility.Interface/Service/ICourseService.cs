using System;
using System.Diagnostics;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// CurrentCourseState
    /// </summary>
    public interface ICurrentCourseStateProvider
    {
        /// <summary>
        /// 当前课程状态
        /// </summary>
        CourseState CurrentCourseState { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICourseService : IService, ICurrentCourseStateProvider
    {

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<CourseStateChangedArgs> CourseStateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="param"></param>
        /// <param name="sender"></param>
        void ChangeState(CourseState state, object param = null, object sender = null);
    }

    /// <summary>
    /// 
    /// </summary>
    public class CourseStateChangedArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseService"></param>
        /// <param name="oldCourseState"></param>
        /// <param name="param"></param>
        [DebuggerStepThrough]
        public CourseStateChangedArgs(ICurrentCourseStateProvider courseService, CourseState oldCourseState, object param = null)
        {
            CourseService = courseService;
            Param = param;
            OldCourseState = oldCourseState;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICurrentCourseStateProvider CourseService { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public CourseState OldCourseState { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public object Param { private set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CourseState
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        Started,

        /// <summary>
        /// 
        /// </summary>
        Stoped,
    }
}