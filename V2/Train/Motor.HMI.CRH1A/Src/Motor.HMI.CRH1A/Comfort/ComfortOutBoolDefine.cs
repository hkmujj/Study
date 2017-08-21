using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Comfort
{
    /// <summary>
    /// 对应 对象配置表的  屏->逻辑 bool量
    /// </summary>
    public enum ComfortOutBoolDefine
    {
        /// <summary>
        ///  MC1车车辆切除/切入 1684
        /// </summary>
        Mc1CutInOut = 1,

        /// <summary>
        /// TP1车车辆切除/切入 1685
        /// </summary>
        Tp1CutInOut,

        /// <summary>
        /// M1车车辆切除/切入  1686
        /// </summary>
        M1CutInOut,

        /// <summary>
        /// M3车车辆切除/切入
        /// </summary>
        M3CutInOut,
        
        /// <summary>
        /// TB车车辆切除/切入
        /// </summary>
        TbCutInOut,


        /// <summary>
        /// M2车车辆切除/切入
        /// </summary>
        M2CutInOut,

        /// <summary>
        /// TP2车车辆切除/切入
        /// </summary>
        Tp2CutInOut,

        Mc2CutInOut,

          /// <summary>
        ///  MC1车车辆HVAC
        /// </summary>
        Mc1Hvac,

        /// <summary>
        /// TP1车车辆HVAC 
        /// </summary>
        Tp1Hvac,

        /// <summary>
        /// M1车车辆HVAC  
        /// </summary>
        M1Hvac,

        /// <summary>
        /// M3车车辆HVAC
        /// </summary>
        M3Hvac,

        
        /// <summary>
        /// TB车车辆HVAC
        /// </summary>
        Tbhvac,


        /// <summary>
        /// M2车车辆HVAC
        /// </summary>
        M2Hvac,

        /// <summary>
        /// TP2车车辆HVAC
        /// </summary>
        Tp2Hvac,

        Mc2Hvac,
    }


    public class ComfortOutBoolDefineHelper
    {

        public static ComfortOutBoolDefine GetOutBoolDefine(int trainIdx, ComfortButtonType btnType)
        {
            if (btnType == ComfortButtonType.HvacControl)
            {
                return (ComfortOutBoolDefine) (trainIdx + 8 + 1);
            }
            return (ComfortOutBoolDefine) trainIdx + 1;
        }
    }


}
