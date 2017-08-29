using System.ComponentModel;

namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    /// 故障等级
    /// </summary>
    public enum EventSystemState
    {
        /// <summary>
        /// 默认
        /// </summary>
        默认,
        /// <summary>
        /// 高压
        /// </summary>
        高压 = 1,
        /// <summary>
        /// 牵引
        /// </summary>
        牵引 = 2,
        /// <summary>
        /// 车门
        /// </summary>
        车门 = 3,
        /// <summary>
        /// 控制和通讯
        /// </summary>
        控制和通讯 = 4,
        /// <summary>
        /// 辅助供电
        /// </summary>
        辅助供电 = 5,
        /// <summary>
        /// 制动
        /// </summary>
        制动 = 6,
        /// <summary>
        /// 空调
        /// </summary>
        空调 = 7,
        /// <summary>
        /// 前部
        /// </summary>
        前部 = 8,
        /// <summary>
        /// 蓄电池供电
        /// </summary>
        蓄电池供电 = 9,
        /// <summary>
        /// 供风
        /// </summary>
        供风 = 10,
        /// <summary>
        /// 卫生设备
        /// </summary>
        卫生设备 = 11,
        /// <summary>
        /// 转向架
        /// </summary>
        转向架 = 12,
        /// <summary>
        /// 信息系统
        /// </summary>
        信息系统 = 13,
        /// <summary>
        /// 火灾探测器
        /// </summary>
        火灾探测器 = 14,
        /// <summary>
        /// 预留1
        /// </summary>
        预留1 = 15,
        /// <summary>
        /// 预留2
        /// </summary>
        预留2 = 16,
        /// <summary>
        /// 预留3
        /// </summary>
        预留3 = 17,
        /// <summary>
        /// 预留4
        /// </summary>
        预留4 = 18,
        /// <summary>
        /// 预留5
        /// </summary>
        预留5 = 19,
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 20,
    }
}
