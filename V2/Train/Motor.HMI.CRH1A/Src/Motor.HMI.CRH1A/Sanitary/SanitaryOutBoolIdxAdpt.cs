using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Sanitary
{
    public class SanitaryOutBoolIdxAdpt
    {
        /// <summary>
        /// 获得排空水箱的bool索引
        /// </summary>
        /// <param name="selectCleanBoxIdx"></param>
        /// <returns></returns>
        public static int GetClearSingleIdx(int selectCleanBoxIdx)
        {
            return selectCleanBoxIdx;
        }

        /// <summary>
        /// 获取排空所有水箱的bool 索引
        /// </summary>
        /// <param name="selectCleanBoxIdx"></param>
        /// <returns></returns>
        public static int GetClearAllIdx(int selectCleanBoxIdx)
        {
            return selectCleanBoxIdx + 8;
        }

    }
}
