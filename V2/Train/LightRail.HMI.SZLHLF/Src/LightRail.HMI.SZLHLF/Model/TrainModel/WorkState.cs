namespace LightRail.HMI.SZLHLF.Model.TrainModel
{
    /// <summary>
    /// 工况
    /// </summary>
    public enum WorkState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 牵引
        /// </summary>
        Traction,
        /// <summary>
        /// 制动
        /// </summary>
        Brake,
        /// <summary>
        /// 惰行
        /// </summary>
        Lazy,
        /// <summary>
        /// 紧急牵引
        /// </summary>
        EmergencyTraction,
        /// <summary>
        /// 紧急制动
        /// </summary>
        EmergencyBrake,
        /// <summary>
        /// 快速制动
        /// </summary>
        FastBrake,

    }
}