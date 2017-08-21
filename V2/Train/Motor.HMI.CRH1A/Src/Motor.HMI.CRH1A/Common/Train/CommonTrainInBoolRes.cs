using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common.Config;

namespace Motor.HMI.CRH1A.Common.Train
{
    public class CommonTrainInBoolRes
    {
        private static CommonTrainInBoolRes _instance;

        public static CommonTrainInBoolRes Instance
        {
            get { return _instance ?? (_instance = new CommonTrainInBoolRes()); }
        }

        private bool[] m_InBools;

        private CommonTrainInBoolRes()
        {

        }

        public void Init(bool[] inbools)
        {
            m_InBools = inbools;
        }

        /// <summary>
        ///  是否有故障
        /// </summary>
        /// <param name="trainRoomidx">从0开始的</param>
        /// <returns></returns>
        public bool IsFault(int trainRoomidx)
        {
            return m_InBools[trainRoomidx + 2];
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool IsActive(TrainConfig.DriverType idx)
        {
            return m_InBools[(int) idx];
        }


    }
}
