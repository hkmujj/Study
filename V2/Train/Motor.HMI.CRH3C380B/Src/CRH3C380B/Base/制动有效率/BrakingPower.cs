using System;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.制动有效率
{
    /// <summary>
    /// 制动有效率计算模块
    /// </summary>
    public class BrakingPower
    {
        private readonly CRH3C380BBase m_SrcObj;

        /// <summary>
        /// 开始计算
        /// </summary>
        /// <param name="bValue">长度必须为32</param>
        /// <param name="marshallMode">是否是16车模式</param>
        public void CalculateResult(bool[] bValue, bool marshallMode)
        {
            if (bValue.Length != 32)
            {
                return;
            }

            StratTime = m_SrcObj.CurrentTime;

            EffectiveRateB = marshallMode ? 16 : 8;
            int tmp = 0;
            for (int i = 0; i < (marshallMode ? 16 : 8); i++)
            {
                BrakePowers0[i] = bValue[i];
                BrakePowers1[i] = bValue[16 + i];
                if (!BrakePowers0[i] || BrakePowers1[i])
                {
                    tmp++;
                }
                EffectiveRateA = EffectiveRateB - tmp;
            }
            decimal a = Convert.ToDecimal(EffectiveRateA);
            decimal b = Convert.ToDecimal(EffectiveRateB);
            EffectiveRateC = Math.Round((a/b)*100, 1);
        }

        /// <summary>
        /// 重置计算模块
        /// </summary>
        public void ResetCalculate()
        {
            BrakePowers0 = new bool[16];
            BrakePowers1 = new bool[16];
            StratTime = new DateTime();
        }

        /// <summary>
        /// 制动有效率有效
        /// </summary>
        public bool[] BrakePowers0 { get; private set; }

        /// <summary>
        /// 制动有效率有效关闭
        /// </summary>
        public bool[] BrakePowers1 { get; private set; }


        public BrakingPower(CRH3C380BBase srcObj)
        {
            m_SrcObj = srcObj;
            BrakePowers0 = new bool[16];
            BrakePowers1 = new bool[16];
        }

        /// <summary>
        /// 有效率开始计算时间
        /// </summary>
        public DateTime StratTime { get; private set; }

        /// <summary>
        /// 制动有效率分子
        /// </summary>
        public int EffectiveRateA { get; private set; }

        /// <summary>
        /// 制动有效率分母
        /// </summary>
        public int EffectiveRateB { get; private set; }

        /// <summary>
        /// 制动有效率结果
        /// </summary>
        public decimal EffectiveRateC { get; private set; }
    }
}
