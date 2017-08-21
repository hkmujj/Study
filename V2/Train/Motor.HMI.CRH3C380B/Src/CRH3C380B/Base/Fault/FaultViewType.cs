namespace Motor.HMI.CRH3C380B.Base.Fault
{
    public enum FaultViewType
    {
        /// <summary>
        /// V > 0
        /// </summary>
        VLargerThan0,

        /// <summary>
        /// V = 0
        /// </summary>
        VEq0,

        /// <summary>
        /// 报告 
        /// </summary>
        Report,

        /// <summary>
        /// 概况
        /// </summary>
        Resume,
    }
}