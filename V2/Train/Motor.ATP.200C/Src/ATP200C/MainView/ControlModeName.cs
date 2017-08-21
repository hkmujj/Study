namespace ATP200C.MainView
{
    /// <summary>
    /// 控制模式名字
    /// </summary>
    public enum ControlModeName
    {
        /// <summary>
        /// 顶棚速度监视区
        /// 限制速度为常数的区域
        /// </summary>
        CSM,

        /// <summary>
        /// 目标速度监视区
        /// 限制速度下降到较低的限制速度值或限制速度为0km/h的目标点的区域
        /// </summary>
        TSM,

        /// <summary>
        /// 开口速度监控
        /// </summary>
        RSM,

        /// <summary>
        /// 其他模式
        /// </summary>
        Other,
    }
}