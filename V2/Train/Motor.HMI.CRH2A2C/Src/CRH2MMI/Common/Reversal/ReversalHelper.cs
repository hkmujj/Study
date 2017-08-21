using CRH2MMI.Common.Config;

namespace CRH2MMI.Common
{
    public class ReversalHelper
    {
        /// <summary>
        /// 获得反转后的列车号
        /// </summary>
        /// <param name="trainNo"></param>
        /// <returns></returns>
        public static int GetReversedTrainNo(int trainNo)
        {
            return TrainConfig.TrainLength - 1 - trainNo;
        }
    }
}
