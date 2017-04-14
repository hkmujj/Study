namespace MMI.Facility.Interface.Data.Config.Form
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActureFormViewConfig
    {
        /// <summary>
        /// 启动视图
        /// </summary>
        int StartViewIndex { get; }

        /// <summary>
        /// 课程开始时视图
        /// </summary>
        int CourseStartViewIndex { get; }

        /// <summary>
        /// 课程结束时视图
        /// </summary>
        int CourseStopViewIndex { get; }

        /// <summary>
        /// 刷新时长
        /// </summary>
        int ViewRfreshInterval { get; }

    }
}