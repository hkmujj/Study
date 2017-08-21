using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Running
{
    /// <summary>
    /// 牵引制动解释
    /// </summary>
    class TowBrakeValueExpression : IFloatValueExpression
    {
        public string Interprete(float value)
        {
            var bt = BitConverter.GetBytes(value);

            return string.Format("{0}{1}{2}", (char) bt[2], (char) bt[1], (char) bt[0]);

        }
    }
}
