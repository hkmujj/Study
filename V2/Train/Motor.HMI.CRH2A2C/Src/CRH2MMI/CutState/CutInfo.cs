using System.ComponentModel;

namespace CRH2MMI.CutState
{
    /// <summary>
    /// 切除的信息
    /// </summary>
    internal enum CutInfo
    {
        /// <summary>
        /// 受电隔离
        /// </summary>
        [Description("受电弓切除")]
        AccessEle,

        /// <summary>
        /// VCB隔离
        /// </summary>
        [Description("VCB切除")]
        VCB,

        /// <summary>
        /// M 车隔离
        /// </summary>
        [Description("M车切除")]
        MCar,

        /// <summary>
        /// 压缩机隔离
        /// </summary>
        [Description("压缩机切除")]
        CondenseMachine,

        /// <summary>
        /// 紧急隔离
        /// </summary>
        [Description("紧急隔离")]
        Emergency,

        /// <summary>
        /// 门状态隔离
        /// </summary>
        [Description("门状态隔离")]
        DoorState,

        /// <summary>
        /// 停放B隔离
        /// </summary>
        [Description("停放B隔离")]
        ParkB,

        /// <summary>
        /// 警惕 隔离
        /// </summary>
        [Description("警惕隔离")]
        Vigilant,
    }
}
