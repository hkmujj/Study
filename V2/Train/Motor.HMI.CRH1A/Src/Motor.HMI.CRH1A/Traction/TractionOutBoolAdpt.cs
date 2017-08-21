using System;
using System.Collections.Generic;
using System.Text;
using Motor.HMI.CRH1A.Traction;

namespace Motor.HMI.CRH1A.Traction1
{
    public class TractionOutBoolAdpt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipMemt"></param>
        /// <returns></returns>
        public static int GetOutBoolIdx(TractionEquipMemtBase equipMemt)
        {
            return equipMemt.TrainNumber * 4 + (int)equipMemt.Type + 80;
        }
    }
}
