namespace Motor.ATP.Infrasturcture.Interface
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
        CutOff = 1,

        /// <summary>
        /// 弱常用制动
        /// </summary>
        WeakNormal = 2,

        /// <summary>
        /// 中等常用制动
        /// </summary>
        MiddlingNormal = 3,

        /// <summary>
        /// 最大常用制动
        /// </summary>
        MaxNormal = 4,

        /// <summary>
        /// 紧急
        /// </summary>
        Emergent = 5,

        /// <summary>
        /// 允许缓解
        /// </summary>
        AllowRelease = 6,
    }

}