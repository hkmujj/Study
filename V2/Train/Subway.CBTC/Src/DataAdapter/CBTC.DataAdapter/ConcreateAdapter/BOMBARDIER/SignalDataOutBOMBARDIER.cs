using CBTC.DataAdapter.Model;

namespace CBTC.DataAdapter.ConcreateAdapter.BOMBARDIER
{
    public class SignalDataOutBOMBARDIER : SignalDataOut
    {
        /// <summary>
        /// ATO模式按钮
        /// </summary>
        public bool ATOModeButton { set; get; }

        /// <summary>
        /// ATB模式按钮
        /// </summary>
        public bool ATBModeButton { set; get; }

        /// <summary>
        /// ATP模式按钮
        /// </summary>
        public bool ATPModeButton { set; get; }

        /// <summary>
        /// RM模式按钮
        /// </summary>
        public bool RMModeButton { set; get; }
        /// <summary>
        /// 允许缓解:信号屏常用制动按钮
        /// </summary>
        public bool CommonBrakeCancel { set; get; }

        public SignalDataOutBOMBARDIER() : base()
        {
            
        }

        public override void ClearInfo()
        {
            
        }
    }
}