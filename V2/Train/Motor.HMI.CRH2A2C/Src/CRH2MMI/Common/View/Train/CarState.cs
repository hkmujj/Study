namespace CRH2MMI.Common.View.Train
{
    enum CarState
    {
        Normal,

        /// <summary>
        /// 牵引
        /// </summary>
        Pull,

        /// <summary>
        /// 电制动
        /// </summary>
        EleBreak,

        Fault,
    }
}
