using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.Infomation
{
    public enum InfomationSystemResonseType
    {
        [Description("")]
        None,

        [Description("输入司机号")]
        InputDriverId,

        [Description("输入车次号")]
        InputTrainId,

        [Description("输入司机号和车次号")]
        InputDriverIdAndTrainId,

        [Description("输入列车数据")]
        InputTrainData,

        [Description("输入时间日期")]
        InputDateTime,

        [Description("输入RBC数据")]
        InputRBCData,

        [Description("选择模式")]
        SelectControlMode,

        [Description("选择载频")]
        SelectFeq,

        [Description("选择等级")]
        SelectCTCS,

        [Description("设置音量")]
        SetVolume,

        [Description("设置亮度")]
        SetLight,
    }
}