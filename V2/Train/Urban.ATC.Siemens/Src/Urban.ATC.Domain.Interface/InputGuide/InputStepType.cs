namespace Motor.ATP.Domain.Interface.InputGuide
{
    /// <summary>
    /// 司机输入，（有流程。）
    /// </summary>
    public enum InputStepType
    {
        None,

        Start,

        SelectC0,
        SelectC1,
        SelectC2,
        SelectC3,
        SelectC3D,

        SelectUpFreq,
        SelectDownFreq,

        /// <summary>
        /// 选择调车
        /// </summary>
        SelectShunting,

        /// <summary>
        /// 选择目视
        /// </summary>
        SelectOnSight,

        /// <summary>
        /// 选择机车信号
        /// </summary>
        SelectCabsignal,
    }
}