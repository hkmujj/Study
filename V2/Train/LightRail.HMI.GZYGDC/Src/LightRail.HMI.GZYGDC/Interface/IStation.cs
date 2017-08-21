using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightRail.HMI.GZYGDC.Interface
{
    /// <summary>
    /// 车站信息接口
    /// </summary>
    public interface IStation
    {
        /// <summary>
        /// 编号
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
