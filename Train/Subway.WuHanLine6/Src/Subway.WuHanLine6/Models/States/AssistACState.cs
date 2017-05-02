namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 辅助系统
    /// </summary>
    public enum AssistACState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,

        /// <summary>
        /// 切除
        /// </summary>
        ACCut,

        /// <summary>
        /// 故障
        /// </summary>
        ACFault,

        /// <summary>
        /// 警告
        /// </summary>
        ACWarn,

        /// <summary>
        /// 运行
        /// </summary>
        ACRun,

        /// <summary>
        /// 断开
        /// </summary>
        ACClose,

        /// <summary>
        /// 切除
        /// </summary>
        DCCut,

        /// <summary>
        /// 故障
        /// </summary>
        DCFault,

        /// <summary>
        /// 警告
        /// </summary>
        DCWarn,

        /// <summary>
        /// 运行
        /// </summary>
        DCRun,

        /// <summary>
        /// 断开
        /// </summary>
        DCClose
    }
}