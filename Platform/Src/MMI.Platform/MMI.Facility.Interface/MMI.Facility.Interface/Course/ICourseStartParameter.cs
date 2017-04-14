using System.Collections.Generic;

namespace MMI.Facility.Interface.Course
{
    /// <summary>
    /// 课程启动参数  
    /// </summary>
    public interface ICourseStartParameter
    {
        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Version, 1.0  , 2.0  ,
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 课程编号
        /// </summary>
        string CourseID { get; }

        /// <summary>
        /// 机车号
        /// </summary>
        string LocomotiveNumber { get; }

        /// <summary>
        /// 信号标识  2.0  > Version 有效
        /// </summary>
        string SignalFlag { get; }

        /// <summary>
        /// 屏的标志, Version > 2.0 有效
        /// </summary>
        List<string> SelectedScreens { get; }
    }
}