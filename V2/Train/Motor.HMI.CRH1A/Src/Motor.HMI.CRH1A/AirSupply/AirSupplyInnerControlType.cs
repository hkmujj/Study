using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.AirSupply
{
    /// <summary>
    /// 供风的内部控件类型
    /// </summary>
    public enum AirSupplyInnerControlType
    {
        /// <summary>
        /// //辅助压缩机
        /// </summary>
        AYaShuoJ,
        /// <summary>
        /// 主压缩机
        /// </summary>
        MYaShuoJi,
        /// <summary>
        /// 压力传感
        /// </summary>
        Presure,

        /// <summary>
        /// 没有
        /// </summary>
        Null,
    }
}
