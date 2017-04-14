namespace MMI.Facility.WPFInfrastructure.ViewModel
{
    /// <summary>
    /// 主窗口的
    /// </summary>
    public interface IMainViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        string WindTitle { set; get; }

        /// <summary>
        /// 宽度
        /// </summary>
        int WindWidth { set; get; }

        /// <summary>
        /// 高度
        /// </summary>
        int WindHeight { set; get; }
    }
}