using System.Collections.Generic;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 逻辑位的比较
    /// </summary>
    public class ExceptionDataLogicCompare : IEqualityComparer<ExceptionData>
    {
        public bool Equals(ExceptionData x, ExceptionData y)
        {
            return x.ExLogNo == y.ExLogNo;
        }

        public int GetHashCode(ExceptionData obj)
        {
            return obj.ExLogNo;
        }
    }
}