namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 制动类型
    /// </summary>
    public enum BrakeType
    {
        /// <summary>
        /// 无制动
        /// </summary>
        None = 0,

        /// <summary>
        /// 切除牵引
        /// </summary>
        CutOff,

        /// <summary>
        /// 弱常用制动
        /// </summary>
        WeakNormal,

        /// <summary>
        /// 中等常用制动
        /// </summary>
        MiddlingNormal,

        /// <summary>
        /// 最大常用制动
        /// </summary>
        MaxNormal,

        /// <summary>
        /// 紧急
        /// </summary>
        Emergent,

        /// <summary>
        /// 允许缓解
        /// </summary>
        AllowRelease,
    }

}