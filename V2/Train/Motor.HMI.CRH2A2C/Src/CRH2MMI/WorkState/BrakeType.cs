using System;
using System.ComponentModel;

namespace CRH2MMI.WorkState
{
    [Flags]
    public enum BrakeType
    {
        None = 0,

        /// <summary>
        /// 紧急
        /// </summary>
        [Description("紧急")]
        Urgency = 1,
        /// <summary>
        /// 常用 
        /// </summary>
        [Description("常用")]
        Nomarl = 2,
        /// <summary>
        /// 快速
        /// </summary>
        [Description("快速")]
        Quickly = 4,
        /// <summary>
        /// 耐雪
        /// </summary>
        [Description("耐雪")]
        EndureSnow = 8,
        /// <summary>
        /// 停放
        /// </summary>
        [Description("停放")]
        Park = 16,

        /// <summary>
        /// 恒速
        /// </summary>
        [Description("恒速")]
        ConstantSpeed = 32,
    }
}
