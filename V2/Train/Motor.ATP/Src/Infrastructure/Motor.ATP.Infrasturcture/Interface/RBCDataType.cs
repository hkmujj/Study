using System;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// RBC数据类型
    /// </summary>
    [Flags]
    public enum RBCDataType
    {
        /// <summary>
        /// RBCid
        /// </summary>
        RBCID = 1,

        /// <summary>
        /// 电话号码
        /// </summary>
        TelNumber = 2,
    }
}