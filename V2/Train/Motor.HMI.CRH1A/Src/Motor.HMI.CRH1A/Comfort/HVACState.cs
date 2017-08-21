namespace Motor.HMI.CRH1A.Comfort
{
    /// <summary>
    ///  HVAC状态 
    /// </summary>
    public enum HvacState
    {
        /// <summary>
        /// 信息显示区域周围无边框(灰色) 表示HVAC工作正常
        /// </summary>
        Normal,

        /// <summary>
        /// 信息显示区域周围是蓝边框表示HVAC切断。
        /// </summary>
        CutOff,

        /// <summary>
        /// 信息显示区域周围是红边框表示HVAC有故障。
        /// </summary>
        Fault,

        /// <summary>
        /// 信息显示区域周围是黄色边框表示HVAC关闭。
        /// </summary>
        TurnOff


    }
}
