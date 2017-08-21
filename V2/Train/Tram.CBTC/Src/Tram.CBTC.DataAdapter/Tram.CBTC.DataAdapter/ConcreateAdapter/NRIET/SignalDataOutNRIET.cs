using Tram.CBTC.DataAdapter.Model;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.NRIET
{
    public class SignalDataOutNRIET : SignalDataOut
    {
        /// <summary>
        /// 强制BM确认
        /// </summary>
        public bool BMACK_Confirm { set; get; }

        /// <summary>
        /// 非强制BM确认
        /// </summary>
        public bool BMUNACK_Confirm { set; get; }

        /// <summary>
        /// 启动完成
        /// </summary>
        public bool StartFinish { set; get; }


        /// <summary>
        ///司机号
        /// </summary>
        public string DriverCode { set; get; }
        
        /// <summary>
        ///车次号
        /// </summary>
        public string TrainCode { set; get; }

        public SignalDataOutNRIET() : base()
        {
            StartFinish = false;
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
            StartFinish = false;
        }
    }
}