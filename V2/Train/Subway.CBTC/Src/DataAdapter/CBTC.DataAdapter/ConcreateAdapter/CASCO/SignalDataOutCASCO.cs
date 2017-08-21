using CBTC.DataAdapter.Model;

namespace CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class SignalDataOutCASCO : SignalDataOut
    {
        /// <summary>
        /// 强制BM确认
        /// </summary>
        public bool BMACK_Confirm { set; get; }

        /// <summary>
        /// 非强制BM确认
        /// </summary>
        public bool BMUNACK_Confirm { set; get; }

        public SignalDataOutCASCO() : base()
        {

        }

        public override void ClearInfo()
        {

        }
    }
}