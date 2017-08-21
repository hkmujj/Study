using System.ComponentModel;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 制动状态
    /// </summary>
    public enum BrakeStatus
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 牵引
        /// </summary>
        [Description("牵引")]
        Traction = 1,
        /// <summary>
        /// 制动
        /// </summary>
        [Description("制动")]
        Brake,
        /// <summary>
        /// 快速制动
        /// </summary>
        [Description("快速制动")]
        FastBrake,
        /// <summary>
        /// 紧急制动施加
        /// </summary>
        [Description("紧急制动")]
        EmergencyBrakeInfliction,
        /// <summary>
        /// 紧急制动未施加
        /// </summary>
        [Description("紧急制动")]
        EmergencyBrakeUnInfliction,
        /// <summary>
        /// 惰行
        /// </summary>
        [Description("惰行")]
        Coasting,
        /// <summary>
        /// 停放制动
        /// </summary>
        [Description("停放制动")]
        ParkingBrake,
        /// <summary>
        /// 紧急牵引
        /// </summary>
        [Description("紧急牵引")]
        EmergencyTraction,
        /// <summary>
        /// 保持制动
        /// </summary>
        [Description("保持制动")]
        KeepBrake,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnow,
    }
    public enum Orientation
    {
        [Description("未知")]
        UnKnow = 0,

        [Description("向前")]
        Before = 1,

        [Description("零位")]
        Zero = 2,

        [Description("向后")]
        Back = 3,

    }

    public enum RunModel
    {
        [Description("ATO")]
        ATO = 1,
        [Description("AMC")]
        AMC = 2,
        [Description("MCS")]
        MCS = 3,
        [Description("RM")]
        RM = 4,
        [Description("NRM")]
        NRM = 5,
        [Description("ATB")]
        ATB = 6,
        [Description("BM")]
        BM = 7,
        [Description("ATP切除")]
        ATPCut = 8,
        [Description("Wash")]
        Wash,
        [Description("预留")]
        Reserve1,
        [Description("预留")]
        Reserve2,
        [Description("未知")]
        UnKnow,
    }
}
