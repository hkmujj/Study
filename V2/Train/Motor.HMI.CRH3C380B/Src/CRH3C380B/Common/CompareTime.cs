using System;

namespace Motor.HMI.CRH3C380B.Common
{
    public static class CompareTime
    {
        public static bool Compare(DateTime t1, DateTime t2, double dPara)
        {
            return t1 - t2 >= TimeSpan.FromSeconds(dPara);
        }
    }
}