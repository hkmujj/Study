using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Station.Overload
{
    enum OverloadState
    {
        /// <summary>
        /// 严重超载
        /// </summary>
        SeriousOverloading,

        /// <summary>
        /// 超载
        /// </summary>
        Overloading,

        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 默认 = 正常
        /// </summary>
        Default = Normal,
    }
}
