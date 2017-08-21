

namespace Subway.WuHanLine6.Models.States
{

    /// <summary>
    /// 门状态
    /// </summary>
    public enum DoorState
    {
        /// <summary>
        /// 门完全关闭
        /// </summary>
        Closed = 0,

        /// <summary>
        /// 门未完全关闭
        /// </summary>
        UnClose,

        /// <summary>
        /// 防挤压过程中
        /// </summary>
        Checked,

        /// <summary>
        /// 门隔离
        /// </summary>
        Isolated,

        /// <summary>
        /// 门紧急解锁
        /// </summary>
        Emergency,

        /// <summary>
        /// 门故障
        /// </summary>
        Fault,

        /// <summary>
        /// 门无效
        /// </summary>
        Invalid,

        /// <summary>
        /// 门通信异常
        /// </summary>
        Communication,
    }
}