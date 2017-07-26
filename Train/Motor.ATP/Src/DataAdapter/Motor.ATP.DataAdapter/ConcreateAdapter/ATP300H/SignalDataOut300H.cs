using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300H
{
    public class SignalDataOut300H : SignalDataOut
    {
        /// <summary>
        /// 请求输入车次号
        /// </summary>
        public bool StartTrainNumSign { set; get; }
        /// <summary>
        /// 请求输入列车长度
        /// </summary>
        public bool StartTrainLengthSign { set; get; }
         /// <summary>
        /// 请求RBC呼叫失败
        /// </summary>
        public bool RBCConnectFailSign { set; get; }
       
        public override void ClearInfo()
        {
            base.ClearInfo();
            
            //C3SelFlag = false;

            //EnterC3SelFlag = false;
        }

    }
}
