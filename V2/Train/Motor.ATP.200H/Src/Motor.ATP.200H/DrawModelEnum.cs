namespace Motor.ATP._200H
{
    /// <summary>
    /// 表盘绘制模式枚举
    /// </summary>
    public enum DrawModelEnum
    {
        /// <summary>
        /// 全部绘制 包括弧形，和刻度
        /// </summary>
        DrawDetail,

        /// <summary>
        /// 绘制刻度
        /// </summary>
        DrawKeDu,

        /// <summary>
        /// 均不绘制
        /// </summary>
        DrawNull
    }
}