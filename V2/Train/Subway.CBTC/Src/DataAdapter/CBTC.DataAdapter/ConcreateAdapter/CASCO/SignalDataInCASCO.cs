using CBTC.DataAdapter.Model;
using System;

namespace CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    [Serializable]
    public class SignalDataInCASCO : SignalDataIn
    {
        /// <summary>
        /// CBTC_BM按钮
        /// </summary>
        public bool CBTC_BM_Button { set; get; }

        /// <summary>
        /// 反馈_强制BM确认
        /// </summary>
        public bool FeedBackBMACK_Confirm { set; get; }

        /// <summary>
        /// 反馈_非强制BM确认
        /// </summary>
        public bool FeedBackBMUNACK_Confirm { set; get; }

        ///// <summary>
        ///// 发车时间
        ///// </summary>
        //public DateTime StopTime { set; get; }

        public SignalDataInCASCO()
        {
            
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}