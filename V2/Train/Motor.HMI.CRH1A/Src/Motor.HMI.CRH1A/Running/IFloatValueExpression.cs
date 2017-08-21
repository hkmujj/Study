using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Running
{
    /// <summary>
    /// float 值解释为 string
    /// </summary>
    interface IFloatValueExpression
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Interprete(float value);
    }
}
