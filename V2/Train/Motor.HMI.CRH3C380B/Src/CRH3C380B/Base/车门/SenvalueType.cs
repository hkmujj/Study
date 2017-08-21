using System.ComponentModel;

namespace Motor.HMI.CRH3C380B.Base.车门
{
    internal enum SenvalueType
    {
        Null,
        [Description("释放司机室门")]
        释放司机室门 = 5162,
        [Description("禁用司机室门")]
        禁用司机室门,
        [Description("释放门")]
        释放门,
        [Description("释放门")]
        禁用门,

    }
}