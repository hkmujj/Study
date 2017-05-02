namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 乘客报警
    /// </summary>
    public enum EmergencyTalkState
    {
        /// <summary>
        /// 乘客紧急通信单元正常，未激活
        /// </summary>
        Normal,

        /// <summary>
        /// 乘客紧急通信单元故障
        /// </summary>
        Fault,

        /// <summary>
        /// 乘客紧急通信单元激活，乘客要求紧急对讲
        /// </summary>
        Active,

        /// <summary>
        /// 乘客紧急通信单元激活，司机已打开通信通道
        /// </summary>
        DriveActive
    }
}