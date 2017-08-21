using System;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common
{
    class CarNameFactory
    {
        public static string GetCarName(int carNo)
        {
            var carIdx = carNo + 1;
            if (carIdx > GlobalParam.CarCount)
            {
                throw new ArgumentOutOfRangeException("carNo", string.Format("carNo must smaller than CarCount(={0})", GlobalParam.CarCount));
            }
            return GlobalParam.CarCount == carIdx ? "00" : carIdx.ToString("00");
        }
    }
}
