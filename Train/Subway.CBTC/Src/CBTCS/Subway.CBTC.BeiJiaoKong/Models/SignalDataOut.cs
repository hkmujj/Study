//using Motor.ATP.DataAdapter.Util;
//using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP.DataAdapter.Model
{
    /// <summary>
    /// 信号屏输出给信号的数据
    /// </summary>
    public class SignalDataOut
    {
        /// <summary>
        /// 强制BM按钮
        /// </summary>
        public bool AckConfirm { set; get; }

        /// <summary>
        /// 非强制BM按钮
        /// </summary>
        public bool UnAckConfirm { set; get; }

        /// <summary>
        /// 硬件确认按钮
        /// </summary>
        public bool HardwareConfirm { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SignalDataOut()
        {
           // StringFloatConverter = new StringFloatConverter();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ClearInfo()
        {
            //DriverCode = "";
            //TrainCode = "";
            //DriverNum = -1;
            //TrainNum = -1;
            //RBCID = -1;
            //RBCNum = -1;
            //TrainLength = -1;
        }
    }
}