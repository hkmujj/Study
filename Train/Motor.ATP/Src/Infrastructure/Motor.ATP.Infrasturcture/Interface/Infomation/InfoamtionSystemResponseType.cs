using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 
    /// </summary>
    public enum InfomationSystemResonseType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        None,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入司机号")]
        InputDriverId,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入车次号")]
        InputTrainId,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入司机号和车次号")]
        InputDriverIdAndTrainId,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入列车数据")]
        InputTrainData,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入时间日期")]
        InputDateTime,

        /// <summary>
        /// 
        /// </summary>
        [Description("输入RBC数据")]
        InputRBCData,

        /// <summary>
        /// 
        /// </summary>
        [Description("选择模式")]
        SelectControlMode,

        /// <summary>
        /// 选择制动测试
        /// </summary>
        [Description("选择制动测试")]
        SelectBrakeTest,

        /// <summary>
        /// 
        /// </summary>
        [Description("选择载频")]
        SelectFeq,

        /// <summary>
        /// 
        /// </summary>
        [Description("选择等级")]
        SelectCTCS,

        /// <summary>
        /// 
        /// </summary>
        [Description("设置音量")]
        SetVolume,

        /// <summary>
        /// 
        /// </summary>
        [Description("设置亮度")]
        SetLight,
    }
}