using CBTC.DataAdapter.Model;
using System;

namespace CBTC.DataAdapter.ConcreateAdapter.THALES
{
    [Serializable]
    public class SignalDataInTHALES : SignalDataIn
    {
        /// <summary>
        /// 下一个车站打开车门的侧
        /// </summary>
        public float NextStationDoorSide { set; get; }

        ///<summary>
        /// 方向手柄
        /// </summary>
        public float MasterControlDirection { set; get; }



        /// <summary>
        /// 信号屏是否显示停站倒计时
        /// </summary>
        public bool IsShowLeftTime { set; get; }

        public SignalDataInTHALES()
        {
            
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}