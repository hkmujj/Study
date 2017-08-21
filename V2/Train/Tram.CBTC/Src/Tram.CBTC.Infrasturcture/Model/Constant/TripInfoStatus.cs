namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 提示信息状态
    /// 0、无
    /// 1、上传日志提示
    /// 2、关机提示
    /// 3、无提示
    /// </summary>
    public enum PromptInfoStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 上传日志提示
        /// </summary>
        UpLoadLog,
        /// <summary>
        /// 关机提示
        /// </summary>
        PowerPrompt,
        /// <summary>
        /// 无提示
        /// </summary>
        NoPrompt
    }
}
